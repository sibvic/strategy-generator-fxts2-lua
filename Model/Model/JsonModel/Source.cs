using System;
using System.Collections.Generic;

namespace ProfitRobots.StrategyGenerator.Model
{
    /// <summary>
    /// Side of the price
    /// </summary>
    public enum Side
    {
        Bid,
        Ask
    }

    /// <summary>
    /// Source
    /// </summary>
    public class Source
    {
        /// <summary>
        /// Identificator of the source.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Source type
        /// </summary>
        public string SourceType { get; set; }

        /// <summary>
        /// Name of the indicator/instrument
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Source identificator of the indicator
        /// </summary>
        [Obsolete]
        public string IndicatorSourceId { get; set; }

        /// <summary>
        /// Source for the indicator
        /// </summary>
        public FormulaItem IndicatorSource { get; set; }

        /// <summary>
        /// Timeframe
        /// </summary>
        public string Timeframe { get; set; }

        /// <summary>
        /// Price type.
        /// </summary>
        public string PriceType { get; set; }

        public const string INDICATOR = "indicator";
        public const string INSTRUMENT = "instrument";
        public const string BID = "bid";
        public const string ASK = "ask";

        /// <summary>
        /// Indicator parameters
        /// </summary>
        public List<StrategyParameter> Parameters { get; set; }
    }
}
