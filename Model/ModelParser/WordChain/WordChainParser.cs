using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ProfitRobots.StrategyGenerator.ModelParser.WordChain
{
    public class WordChainParser
    {
        List<IExpectedItem> _items = new List<IExpectedItem>();
        IEnumerator<IExpectedItem> _currentItem;
        Dictionary<string, object> _values = new Dictionary<string, object>();

        public WordChainParser ExpectWord(string word, bool optional = false)
        {
            _items.Add(new WordExpected(word, optional));
            return this;
        }

        public WordChainParser ExpectWord(Regex word, string name = null)
        {
            _items.Add(new RegexWordExpected(word, name));
            return this;
        }

        public WordChainParser ExpectDouble(string name)
        {
            _items.Add(new DoubleExpected(name));
            return this;
        }

        public WordChainParser ExpectInteger(string name)
        {
            _items.Add(new IntegerExpected(name));
            return this;
        }

        public T GetValue<T>(string name)
        {
            return (T)_values[name];
        }

        bool _finished = false;
        List<Token> _tokens = new List<Token>();

        public IEnumerable<Token> Tokens => _tokens;

        /// <summary>
        /// Parse a word
        /// </summary>
        /// <param name="word">Word to parse</param>
        /// <exception cref="InvalidPhraseItemException">On invalid item.</exception>
        /// <returns>Status of the parsing</returns>
        public ParsingStatus ParseWord(Token word)
        {
            if (_finished)
                return ParsingStatus.Finished;
            if (_currentItem == null)
            {
                _currentItem = _items.GetEnumerator();
                _currentItem.Reset();
                _currentItem.MoveNext();
            }
            while (!_currentItem.Current.Parse(word.Data))
            {
                if (!_currentItem.Current.IsOptional)
                {
                    Reset();
                    return ParsingStatus.Failed;
                }
                if (!_currentItem.MoveNext())
                {
                    break;
                }
            }
            _tokens.Add(word);
            if (!_currentItem.MoveNext())
            {
                SaveResults();
                _finished = true;
                return ParsingStatus.Finished;
            }
            return ParsingStatus.Parsed;
        }

        internal void Reset()
        {
            foreach (var item in _items)
            {
                item.Reset();
            }
            _tokens.Clear();
            _currentItem = null;
            _values.Clear();
            _finished = false;
        }

        private void SaveResults()
        {
            foreach (var item in _items)
            {
                (string name, object value) = item.Result;
                if (name != null && value != null)
                {
                    _values.Add(name, value);
                }
            }
        }
    }
}
