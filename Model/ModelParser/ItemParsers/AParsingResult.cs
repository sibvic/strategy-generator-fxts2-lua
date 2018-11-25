using System.Collections.Generic;

namespace ProfitRobots.StrategyGenerator.ModelParser
{
    public abstract class AParsingResult : IParsingResult
    {
        public abstract ParsingResultType Type { get; }

        public int Priority { get; set; }

        List<Token> _tokens = new List<Token>();

        public IEnumerable<Token> Tokens
        {
            get
            {
                return _tokens;
            }
        }
        public void AddToken(Token token)
        {
            _tokens.Add(token);
        }
    }
}
