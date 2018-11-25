using ProfitRobots.StrategyGenerator.Model.ModelParser;

namespace ProfitRobots.StrategyGenerator.Model
{
    /// <summary>
    /// Thrown when language doesn't support specified parameter type.
    /// </summary>
    public class NotSupportedParameterException : StrategyGeneratorException
    {
        public NotSupportedParameterException(ParameterType parameterType, string language)
            :base(parameterType.GetDisplayName() + " not supported in " + language)
        {
        }
    }
}
