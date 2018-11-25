namespace ProfitRobots.StrategyGenerator.Model
{
    public class MainInstrumentSource : IMetaSource
    {
        public SourceType SourceType => SourceType.MainInstrument;

        public string Id { get; }
    }
}
