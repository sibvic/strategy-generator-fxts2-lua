﻿using System.Collections.Generic;

namespace ProfitRobots.StrategyGenerator.Model
{
    /// <summary>
    /// Base interface for a condition
    /// </summary>
    public class Condition
    {
        /// <summary>
        /// Type of the condition.
        /// </summary>
        public string ConditionType { get; set; }

        public string Id { get; set; }

        /// <summary>
        /// Description entered by the user.
        /// This class should be generated by this description.
        /// </summary>
        public string UserInput { get; set; }

        public const string CROSSES = "crosses";
        public const string CROSSES_OVER = "crossesOver";
        public const string CROSSES_OVER_OR_TOUCH = "crossesOverOrTouch";
        public const string CROSSES_UNDER = "crossesUnder";
        public const string CROSSES_UNDER_OR_TOUCH = "crossesUnderOrTouch";
        public const string OR = "or";
        public const string AND = "and";
        public const string GT = ">";
        public const string LT = "<";
        public const string GTE = ">=";
        public const string LTE = "<=";
        public const string EQ = "==";
        public const string NEQ = "~=";

        /// <summary>
        /// List of subconditions
        /// </summary>
        public List<Condition> Conditions { get; set; }

        /// <summary>
        /// Contains either the value either the name of the stream.
        /// </summary>
        public FormulaItem Arg1 { get; set; }

        /// <summary>
        /// Contains either the value either the name of the stream.
        /// </summary>
        public FormulaItem Arg2 { get; set; }
    }
}