namespace ProfitRobots.StrategyGenerator.Model
{
    public class TwoArgumentCondition : ICondition
    {
        /// <summary>
        /// Type of the condition.
        /// </summary>
        public ConditionType ConditionType { get; internal set; }

        /// <summary>
        /// Contains either the value either the name of the stream.
        /// </summary>
        public MetaFormulaItem Arg1 { get; internal set; }

        /// <summary>
        /// Contains either the value either the name of the stream.
        /// </summary>
        public MetaFormulaItem Arg2 { get; internal set; }
    }
}
