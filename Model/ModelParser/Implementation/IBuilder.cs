using ProfitRobots.StrategyGenerator.ModelParser.Implementation;

namespace ProfitRobots.StrategyGenerator.ModelParser
{
    interface IBuilder
    {
        /// <summary>
        /// Tries to add a word into the builder.
        /// </summary>
        /// <param name="word">Word to add.</param>
        /// <returns>True if the word was successfully added. False if the work is not a condition word.</returns>
        bool TryAddWord(Token word);
    }
}
