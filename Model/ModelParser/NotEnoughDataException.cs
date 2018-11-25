using ProfitRobots.StrategyGenerator.Model;

namespace ProfitRobots.StrategyGenerator.ModelParser
{
    /// <summary>
    /// Thrown when there is not enouth data in the parse string to build the item.
    /// </summary>
    public class NotEnoughDataException : ParserException
    {
        public NotEnoughDataException(StrategyModel model, string missingItem = null, string message = null)
            :base(model, missingItem, message: message ?? $"Missing {missingItem}")
        {
        }
    }
}
