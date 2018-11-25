namespace ProfitRobots.StrategyGenerator.Model
{
    /// <summary>
    /// Generates strategy code.
    /// </summary>
    public interface IStrategyGenerator
    {
        /// <summary>
        /// Generates strategy code based on strategy model
        /// </summary>
        /// <param name="model">Strategy model</param>
        /// <exception cref="NotSupportedConditionException">Generator doesn't support the condition used in the model.</exception>
        /// <exception cref="NotSupportedParameterException">Generator doesn't support the parameter used in the model.</exception>
        /// <exception cref="NotSupportedActionException">Generator doesn't support the action used in the model.</exception>
        /// <exception cref="StrategyGeneratorException">When the model is invalid</exception>
        /// <returns>Strategy code</returns>
        string Generate(MetaModel model);

        /// <summary>
        /// Geneerates strategy name based on the strategy name.
        /// </summary>
        /// <param name="strategyName">Strategy name</param>
        /// <returns>File name with the proper extension.</returns>
        string GenerateFileName(string strategyName);
    }
}
