using System;

namespace ProfitRobots.StrategyGenerator.Model
{
    /// <summary>
    /// Base exception for the strategy generator.
    /// </summary>
    public class StrategyGeneratorException : Exception
    {
        public StrategyGeneratorException(string message)
            : base(message)
        {

        }
    }
}
