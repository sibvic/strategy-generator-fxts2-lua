using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitRobots.StrategyGenerator.Model
{
    /// <summary>
    /// Indicator
    /// </summary>
    public class Indicator
    {
        /// <summary>
        /// Name of the indicator (ID in the FXCM terminology), like MVA
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Name of the variable where indicator is located.
        /// </summary>
        public string VariableName { get; set; }
    }
}
