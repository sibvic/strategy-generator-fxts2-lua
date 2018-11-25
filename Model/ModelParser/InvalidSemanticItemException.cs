using ProfitRobots.StrategyGenerator.Model;

namespace ProfitRobots.StrategyGenerator.ModelParser
{
    /// <summary>
    /// Thrown when an invalid semantic item is found during the parding.
    /// </summary>
    public class InvalidSemanticItemException : ParserException
    {
        public string ItemName { get; internal set; }

        public InvalidSemanticItemException(StrategyModel model, string itemName, string parseText = null, string message = null)
            :base(model, parseText: parseText, message: message ?? $"Invalid semantic item: {itemName}")
        {
            ItemName = itemName;
        }
    }
}
