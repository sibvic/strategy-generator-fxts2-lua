using ProfitRobots.StrategyGenerator.Model;

namespace ProfitRobots.StrategyGenerator.ModelParser
{
    class SourceIdGenerator
    {
        int _indicatorSourcesCount = 0;
        int _instrumentSourcesCount = 0;
        public string Generate(IMetaSource source)
        {
            switch (source.SourceType)
            {
                case SourceType.Indicator:
                    return string.Format("indicator_{0}", ++_indicatorSourcesCount);
                case SourceType.Instrument:
                    return string.Format("instrument_{0}", ++_instrumentSourcesCount);
            }
            return "source";
        }
    }
}
