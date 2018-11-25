namespace ProfitRobots.StrategyGenerator.ModelParser.WordChain
{
    /// <summary>
    /// Interface for the expected item
    /// </summary>
    interface IExpectedItem
    {
        /// <summary>
        /// Try to parse the word.
        /// </summary>
        /// <param name="word">Word to parse</param>
        /// <returns>true if the word is expected</returns>
        bool Parse(string word);

        /// <summary>
        /// Get the parsed result.
        /// </summary>
        (string name, object value) Result { get; }

        /// <summary>
        /// Whether the expected item is optional
        /// </summary>
        bool IsOptional { get; }

        /// <summary>
        /// Reset the parsed value.
        /// </summary>
        void Reset();
    }
}
