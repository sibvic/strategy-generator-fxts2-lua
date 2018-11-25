using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitRobots.StrategyGenerator.ModelParser
{
    public class Token
    {
        public string Data { get; set; }

        List<IParsingResult> _results = new List<IParsingResult>();
        public List<IParsingResult> Results => _results;

        public void AddResult(IParsingResult result)
        {
            _results.Add(result);
            result.AddToken(this);
        }

        public enum Type
        {
            Word,
            Number,
            Operator
        }

        public Type TokenType { get; internal set; }
    }

}
