namespace ProfitRobots.StrategyGenerator.Model
{
    /// <summary>
    /// Parameters (could be set by the user).
    /// </summary>
    public class StrategyParameter
    {
        /// <summary>
        /// Id of the parameter
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of the parameter. Visible to the user
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the parameter. Visible to the user
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Default Value.
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Type of the parameter.
        /// </summary>
        public string ParameterType { get; set; }

        public const string INTEGER = "int";
        public const string PRICE_TYPE = "price_type";
        public const string PRICE_TYPE_BID = "bid";
        public const string PRICE_TYPE_ASK = "ask";
        public const string TIMEFRAME = "timeframe";
        public const string BOOLEAN = "bool";
        public const string STRING = "string";
        public const string PRICE = "price";
        public const string DOUBLE = "double";
        public const string EXTERNAL_PARAMETER = "external_param";
    }
}
