namespace ProfitRobots.StrategyGenerator.Model
{
    /// <summary>
    /// Trading action.
    /// </summary>
    public class Action
    {
        /// <summary>
        /// Identificator of the action
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Action type
        /// </summary>
        public string ActionType { get; set; }

        /// <summary>
        /// Name of the action. Used in parameters.
        /// </summary>
        public string Name { get; set; }

        public const string BUY = "buy";
        public const string SELL = "sell";
        public const string ENTRY_BUY = "entry_buy";
        public const string ENTRY_SELL = "entry_sell";
        public const string CUSTOM = "*";
        public const string EXIT = "exit";
        public const string EXIT_BUY = "exit_buy";
        public const string EXIT_SELL = "exit_sell";

        /// <summary>
        /// List of conditions for the actions to trigger.
        /// </summary>
        public Condition Condition { get; set; }

        /// <summary>
        /// Rate for the entry order.
        /// </summary>
        public Order Entry { get; set; }

        /// <summary>
        /// Stop order.
        /// </summary>
        public Order Stop { get; set; }

        /// <summary>
        /// Limit order
        /// </summary>
        public Order Limit { get; set; }
    }
}
