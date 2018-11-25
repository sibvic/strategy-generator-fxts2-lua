namespace ProfitRobots.StrategyGenerator.Model
{
    public class InstrumentSource : IMetaSource
    {
        public SourceType SourceType => SourceType.Instrument;

        public string Id { get; set;  }

        /// <summary>
        /// Timeframe
        /// </summary>
        public string Timeframe { get; set; }

        /// <summary>
        /// Price type.
        /// </summary>
        public string PriceType { get; set; }
    }
}
