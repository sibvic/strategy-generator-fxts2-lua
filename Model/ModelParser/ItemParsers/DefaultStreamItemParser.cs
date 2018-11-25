using ProfitRobots.StrategyGenerator.ModelParser.WordChain;
using System.Text.RegularExpressions;

namespace ProfitRobots.StrategyGenerator.ModelParser.ItemParsers
{
    /// <summary>
    /// Default stream: open, high, low or close
    /// </summary>
    class DefaultStreamParsingResult : AParsingResult
    {
        public override ParsingResultType Type => ParsingResultType.DefaultStream;

        public string StreamName { get; set; }
    }

    /// <summary>
    /// Parses the default stream: open, high, low or close
    /// </summary>
    class DefaultStreamItemParser : IItemParser
    {
        WordChainParser _main;

        public DefaultStreamItemParser()
        {
            _main = new WordChainParser()
                .ExpectWord(new Regex("^(open|high|low|close)$"), "name");
        }

        public bool ParseWord(Token word)
        {
            if (_main.ParseWord(word) == ParsingStatus.Finished)
            {
                FinishParsing(_main.GetValue<string>("name"));
                return true;
            }
            return false;
        }

        private void FinishParsing(string name)
        {
            var result = new DefaultStreamParsingResult()
            {
                StreamName = name,
                Priority = 10
            };
            foreach (var token in _main.Tokens)
            {
                token.AddResult(result);
            }
            _main.Reset();
        }
    }
}
