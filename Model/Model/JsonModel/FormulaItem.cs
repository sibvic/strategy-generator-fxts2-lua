namespace ProfitRobots.StrategyGenerator.Model
{
    /// <summary>
    /// Describes argument for the condition.
    /// </summary>
    public class FormulaItem
    {
        /// <summary>
        /// Contains either the value either the name of the stream.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Type of the value.
        /// </summary>
        public string ValueType { get; set; }

        /// <summary>
        /// If the Value is a stream this field may contain name of the substream (like "close")
        /// </summary>
        public string Substream { get; set; }

        /// <summary>
        /// Type of the used stream
        /// </summary>
        public string StreamType { get; set; }

        /// <summary>
        /// Period shift for the value stream. The latest value will be used if empty.
        /// </summary>
        public string PeriodShift { get; set; }

        /// <summary>
        /// Period shift source. 
        /// Period could be shifted on another timeframe and/or source (which could have skipped bars).
        /// </summary>
        public string PeriodShiftSource { get; set; }

        /// <summary>
        /// What the user has typed
        /// </summary>
        public string UsersInput { get; set; }

        public const string NOT_USED = null;
        public const string INDICATOR = "indicator";
        public const string INSTRUMENT = "instrument";

        public const string OPERAND = "op";
        public const string STREAM = "stream";
        public const string STREAM_VALUE = "streamValue";
        public const string PARAM = "param";
        public const string VALUE = null;

        public static FormulaItem BuildeOperand(string value)
        {
            return new FormulaItem()
            {
                Value = value,
                ValueType = OPERAND,
                StreamType = NOT_USED
            };
        }
    }
}
