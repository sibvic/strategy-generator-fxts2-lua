using System.Collections;
using System.Collections.Generic;

namespace ProfitRobots.StrategyGenerator.ModelParser.Implementation
{
    class Tokens : IEnumerable<Token>
    {
        public static Tokens Parse(string text)
        {
            return new Tokens(text);
        }

        private Tokens(string text)
        {
            _text = text;
        }
        string _text;

        public IEnumerator<Token> GetEnumerator()
        {
            return new TokenIterator(_text);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new TokenIterator(_text);
        }
    }
    /// <summary>
    /// Get tokes from text.
    /// </summary>
    class TokenIterator : IEnumerator<Token>
    {
        public TokenIterator(string text)
        {
            _text = text;
        }
        string _text;
        int _currentIndex = 0;
        ITokenBuilder _tokenBuilder;

        interface ITokenBuilder
        {
            bool AddChar(char ch);

            Token Build();
        }

        class EmptyBuilder : ITokenBuilder
        {
            public bool AddChar(char ch) => ch == ' ';

            public Token Build() => null;
        }

        class NumberTokenBuilder : ITokenBuilder
        {
            string _value = "";

            public bool AddChar(char ch)
            {
                switch (ch)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case '.':
                    case ',':
                        _value += ch;
                        return true;
                }
                return false;
            }

            public Token Build() => new Token()
            {
                Data = _value,
                TokenType = Token.Type.Number
            };
        }

        class OperatorTokenBuilder : ITokenBuilder
        {
            string _value = "";

            public static bool SupportsChar(char ch)
            {
                switch (ch)
                {
                    case '+':
                    case '-':
                    case '*':
                    case '/':
                    case '<':
                    case '>':
                    case '=':
                    case '!':
                        return true;
                }
                return false;
            }

            public bool AddChar(char ch)
            {
                if (SupportsChar(ch))
                {
                    _value += ch;
                    return true;
                }
                return false;
            }

            public Token Build() => new Token()
            {
                Data = _value,
                TokenType = Token.Type.Operator
            };
        }

        class EntityTokenBuilder : ITokenBuilder
        {
            string _value = "";

            public bool AddChar(char ch)
            {
                if (char.IsLetterOrDigit(ch) || ch == '.' || ch == '_')
                {
                    _value += ch;
                    return true;
                }
                return false;
            }

            public Token Build() => new Token()
            {
                Data = _value,
                TokenType = Token.Type.Word
            };
        }

        class TokenBuilderFactory
        {
            public static ITokenBuilder Create(char ch)
            {
                return CreateBuilder(ch);
            }

            private static ITokenBuilder CreateBuilder(char ch)
            {
                if (char.IsNumber(ch))
                {
                    return new NumberTokenBuilder();
                }
                if (OperatorTokenBuilder.SupportsChar(ch))
                {
                    return new OperatorTokenBuilder();
                }
                if (!char.IsWhiteSpace(ch))
                {
                    return new EntityTokenBuilder();
                }
                return new EmptyBuilder();
            }
        }

        public Token Current => _tokenBuilder?.Build();
        object IEnumerator.Current => _tokenBuilder?.Build();

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (_currentIndex >= _text.Length)
                return false;
            _tokenBuilder = null;
            while (_currentIndex < _text.Length)
            {
                if (!AddChar(_text[_currentIndex]))
                {
                    if (_tokenBuilder is EmptyBuilder)
                    {
                        _tokenBuilder = null;
                        continue;
                    }
                    break;
                }
                _currentIndex += 1;
            }
            return _tokenBuilder != null && !(_tokenBuilder is EmptyBuilder);
        }

        private bool AddChar(char v)
        {
            if (_tokenBuilder == null)
            {
                _tokenBuilder = TokenBuilderFactory.Create(v);
            }
            return _tokenBuilder.AddChar(v);
        }

        public void Reset()
        {
            _currentIndex = 0;
        }
    }
}
