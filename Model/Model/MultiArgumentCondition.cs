using System.Collections.Generic;

namespace ProfitRobots.StrategyGenerator.Model
{
    public class MultiArgumentCondition : ICondition
    {
        /// <summary>
        /// Type of the condition.
        /// </summary>
        public ConditionType ConditionType { get; internal set; }

        /// <summary>
        /// List of subconditions
        /// </summary>
        public List<ICondition> Subconditions { get; internal set; }
    }
}
