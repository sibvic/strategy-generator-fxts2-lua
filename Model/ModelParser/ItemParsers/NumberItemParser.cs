using ProfitRobots.StrategyGenerator.ModelParser.WordChain;

namespace ProfitRobots.StrategyGenerator.ModelParser.ItemParsers
{
    /// <summary>
    /// Number
    /// </summary>
    class NumberParsingResult : AParsingResult
    {
        public override ParsingResultType Type => ParsingResultType.Number;

        public double Number { get; set; }
    }

    /// <summary>
    /// Parses a number
    /// </summary>
    class NumberItemParser : IItemParser
    {
        WordChainParser _main;

        public NumberItemParser()
        {
            _main = new WordChainParser()
                .ExpectDouble("number");
        }

        public bool ParseWord(Token word)
        {
            if (_main.ParseWord(word) == ParsingStatus.Finished)
            {
                FinishParsing(_main.GetValue<double>("number"));
                return true;
            }
            return false;
        }

        private void FinishParsing(double number)
        {
            var result = new NumberParsingResult()
            {
                Number = number,
                Priority = 1
            };
            foreach (var token in _main.Tokens)
            {
                token.AddResult(result);
            }
            _main.Reset();
        }
    }
}
