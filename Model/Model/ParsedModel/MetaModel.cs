using System.Collections.Generic;

namespace ProfitRobots.StrategyGenerator.Model
{
    /// <summary>
    /// Adds meta information for the strategy model.
    /// Contains processed information about a strategy, e.i. slit by the building blocks: parameters, variables etc..
    /// </summary>
    public class MetaModel
    {
        /// <summary>
        /// Name of the strategy.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the strategy.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// List of indicator instances used by the strategy.
        /// </summary>
        public List<Indicator> Indicators { get; set; }

        /// <summary>
        /// List of actions.
        /// </summary>
        public List<MetaAction> Actions { get; set; }

        /// <summary>
        /// List of parameters.
        /// </summary>
        public List<IParameter> Parameters { get; set; }

        /// <summary>
        /// List of sources.
        /// </summary>
        public List<IMetaSource> Sources { get; set; }

        /// <summary>
        /// Whether to print debug information.
        /// </summary>
        public bool Debug { get; set; }

        /// <summary>
        /// List of modules.
        /// </summary>
        public List<MetaModule> Modules { get; set; }
    }
}
