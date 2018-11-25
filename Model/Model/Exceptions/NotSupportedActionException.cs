using ProfitRobots.StrategyGenerator.Model.ModelParser;

namespace ProfitRobots.StrategyGenerator.Model
{
    /// <summary>
    /// Thrown on not supported action.
    /// </summary>
    public class NotSupportedActionException : StrategyGeneratorException
    {
        public NotSupportedActionException(MetaActionType actionType, string language)
            : base(actionType.GetDisplayName() + " not supported in " + language)
        {

        }
    }
}
