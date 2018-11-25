using System.Collections.Generic;

namespace ProfitRobots.StrategyGenerator.Model
{
    /// <summary>
    /// Strategy model. Describes all the behavior and features of the strategy
    /// </summary>
    public class StrategyModel
    {
        /// <summary>
        /// Name of the strategy.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Brief description of the strategy.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Strategy actions.
        /// </summary>
        public List<Action> Actions { get; set; }

        /// <summary>
        /// Used sources.
        /// </summary>
        public List<Source> Sources { get; set; }

        /// <summary>
        /// Modules.
        /// </summary>
        public List<Module> Modules { get; set; }

        /// <summary>
        /// List of parameters.
        /// </summary>
        public List<StrategyParameter> Parameters { get; set; }

        /// <summary>
        /// Whether to use debug mode. Could be true/false/null.
        /// </summary>
        public string Debug { get; set; }
    }
}
