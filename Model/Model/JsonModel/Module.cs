using System.Collections.Generic;

namespace ProfitRobots.StrategyGenerator.Model
{
    /// <summary>
    /// Strategy module
    /// </summary>
    public class Module
    {
        /// <summary>
        /// Name of the module
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Parameters for the module.
        /// </summary>
        public List<StrategyParameter> Parameters { get; set; }
    }
}
