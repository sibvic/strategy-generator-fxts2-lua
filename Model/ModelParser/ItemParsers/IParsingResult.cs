using System.Collections.Generic;

namespace ProfitRobots.StrategyGenerator.ModelParser
{
    public interface IParsingResult
    {
        ParsingResultType Type { get; }

        int Priority { get; }

        void AddToken(Token token);

        IEnumerable<Token> Tokens { get; }
    }
}
