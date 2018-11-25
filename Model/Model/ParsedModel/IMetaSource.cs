namespace ProfitRobots.StrategyGenerator.Model
{
    public enum SourceType
    {
        Indicator,
        Instrument,
        MainInstrument
    }

    public interface IMetaSource
    {
        /// <summary>
        /// Source type
        /// </summary>
        SourceType SourceType { get; }

        /// <summary>
        /// Identificator of the source
        /// </summary>
        string Id { get; }
    }
}