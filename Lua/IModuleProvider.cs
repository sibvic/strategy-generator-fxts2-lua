namespace ProfitRobots.StrategyGenerator.Lua
{
    //TODO: move outside of this library
    /// <summary>
    /// Provides code for modules.
    /// </summary>
    public interface IModuleProvider
    {
        /// <summary>
        /// Get code for module.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <returns>Code of the module</returns>
        string[] GetCode(string moduleName);
    }
}
