using ProfitRobots.StrategyGenerator.Model.ModelParser;

namespace ProfitRobots.StrategyGenerator.Model
{
    public class NotSupportedSourceException : StrategyGeneratorException
    {
        public NotSupportedSourceException(SourceType sourceType)
            : base("Unsupported source: " + sourceType.GetDisplayName())
        {

        }

        public NotSupportedSourceException(string message)
            :base(message)
        {
        }
    }
}
