namespace ProfitRobots.StrategyGenerator.Model
{
    /// <summary>
    /// Add meta information for the action
    /// </summary>
    public class MetaAction
    {
        /// <summary>
        /// Name of the action.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id of the action.
        /// </summary>
        public string ActionId { get; set; }

        /// <summary>
        /// Action type.
        /// </summary>
        public MetaActionType ActionType { get; set; }

        /// <summary>
        /// Condition
        /// </summary>
        public ICondition Condition { get; set; }

        /// <summary>
        /// Entry rate for the trade action.
        /// </summary>
        public MetaOrder Entry { get; set; }

        /// <summary>
        /// Stop for the trade action.
        /// </summary>
        public MetaOrder Stop { get; set; }

        /// <summary>
        /// Limit for the trade action.
        /// </summary>
        public MetaOrder Limit { get; set; }
    }
}
