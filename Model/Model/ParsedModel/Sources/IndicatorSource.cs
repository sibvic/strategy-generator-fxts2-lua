using ProfitRobots.StrategyGenerator.Model;
using System.Collections.Generic;

namespace ProfitRobots.StrategyGenerator.Model
{
    public class IndicatorSource : IMetaSource
    {
        public SourceType SourceType => SourceType.Indicator;

        public string Id { get; set; }

        /// <summary>
        /// Name of the indicator
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Source of the indicator
        /// </summary>
        public MetaFormulaItem Source { get; set; }

        /// <summary>
        /// Parameters
        /// </summary>
        public List<IParameter> Parameters { get; private set; } = new List<IParameter>();
    }
}
