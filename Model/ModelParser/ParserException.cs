using ProfitRobots.StrategyGenerator.Model;
using System;

namespace ProfitRobots.StrategyGenerator.ModelParser
{
    /// <summary>
    /// General parser exception
    /// </summary>
    public class ParserException : Exception
    {
        public StrategyModel Model { get; internal set; }
        public string ParseText { get; internal set; }

        public ParserException(StrategyModel model, string parseText = null, string message = null)
            :base(message)
        {
            Model = model;
            ParseText = parseText;
        }
    }
}
