using System.Collections.Generic;

namespace ProfitRobots.StrategyGenerator.Model
{
    /// <summary>
    /// Order
    /// </summary>
    public class MetaOrder
    {
        /// <summary>
        /// Order value. May contain a formula.
        /// </summary>
        public List<MetaFormulaItem> ValueStack { get; set; }
    }
}
