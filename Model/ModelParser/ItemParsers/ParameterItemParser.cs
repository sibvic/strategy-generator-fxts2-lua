using ProfitRobots.StrategyGenerator.ModelParser.WordChain;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProfitRobots.StrategyGenerator.ModelParser.ItemParsers
{
    /// <summary>
    /// Parameter
    /// </summary>
    class ParameterParsingResult : AParsingResult
    {
        public override ParsingResultType Type => ParsingResultType.Parameter;

        public string ParameterName { get; set; }
    }
    /// <summary>
    /// Parses parameter
    /// </summary>
    class ParameterItemParser : IItemParser
    {
        WordChainParser _main;

        public ParameterItemParser(System.Collections.Generic.IEnumerable<Model.StrategyParameter> parameters)
        {
            var allNames = new Regex("^(" + string.Join("|", parameters.Select(p => p.Id)) + ")$");
            _main = new WordChainParser()
                .ExpectWord(allNames, "name");
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
            var result = new ParameterParsingResult()
            {
                ParameterName = name,
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
