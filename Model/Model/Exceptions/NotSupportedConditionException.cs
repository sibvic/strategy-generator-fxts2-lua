using ProfitRobots.StrategyGenerator.Model.ModelParser;

namespace ProfitRobots.StrategyGenerator.Model
{
    /// <summary>
    /// Thrown when the language doesn't support specified condition.
    /// </summary>
    public class NotSupportedConditionException : StrategyGeneratorException
    {
        public NotSupportedConditionException(ConditionType conditionType, string language)
            :base(conditionType.GetDisplayName() + " not supported in " + language)
        {

        }
    }
}
