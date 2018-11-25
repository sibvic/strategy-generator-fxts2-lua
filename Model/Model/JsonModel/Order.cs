using System;
using System.Collections.Generic;

namespace ProfitRobots.StrategyGenerator.Model
{
    /// <summary>
    /// Order
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Order for the order. May contain a formula
        /// </summary>
        [Obsolete]
        public string Value { get; set; }

        /// <summary>
        /// Contains stack of values and operations needed to calculate the order value.
        /// </summary>
        public List<FormulaItem> ValueStack { get; set; }

        /// <summary>
        /// User input.
        /// </summary>
        public string UserInput { get; set; }
    }
}
