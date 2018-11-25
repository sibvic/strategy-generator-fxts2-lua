namespace ProfitRobots.StrategyGenerator.ModelParser
{
    public interface IItemParser
    {
        /// <summary>
        /// Parse a word
        /// </summary>
        /// <param name="word">Word to parse</param>
        bool ParseWord(Token word);
    }
}
