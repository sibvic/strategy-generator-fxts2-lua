using ProfitRobots.StrategyGenerator.Model;
using ProfitRobots.StrategyGenerator.ModelParser.Implementation;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ProfitRobots.StrategyGenerator.ModelParser
{
    /// <summary>
    /// Builds a compare operation
    /// </summary>
    class CompareOperationBuilder : IBuilder
    {
        List<string> _words = new List<string>();
        
        public bool TryAddWord(Token word)
        {
            switch (word.Data)
            {
                case "cross":
                case "crosses":
                case "over":
                case "under":
                case "<":
                case "<=":
                case ">":
                case ">=":
                case "=":
                case "==":
                case "<>":
                case "!=":
                case "~=":
                    _words.Add(word.Data);
                    return true;
            }
            return false;
        }

        static Tuple<Regex, string>[] _conditions =
        {
            new Tuple<Regex, string>(new Regex("^cross(es)?$"), Condition.CROSSES),
            new Tuple<Regex, string>(new Regex("^cross(es)? over$"), Condition.CROSSES_OVER),
            new Tuple<Regex, string>(new Regex("^cross(es)? under$"), Condition.CROSSES_UNDER),
            new Tuple<Regex, string>(new Regex("^<$"), Condition.LT),
            new Tuple<Regex, string>(new Regex("^>$"), Condition.GT),
            new Tuple<Regex, string>(new Regex("^<=$"), Condition.LTE),
            new Tuple<Regex, string>(new Regex("^>=$"), Condition.GTE),
            new Tuple<Regex, string>(new Regex("^==?$"), Condition.EQ),
            new Tuple<Regex, string>(new Regex("^<>$"), Condition.NEQ),
            new Tuple<Regex, string>(new Regex("^!=$"), Condition.NEQ),
            new Tuple<Regex, string>(new Regex("^~=$"), Condition.NEQ)
        };
        /// <summary>
        /// Builds a condition.
        /// </summary>
        /// <exception cref="InvalidSemanticItemException">On invalid condition description</exception>
        /// <returns>Condition ID (in terms of StrategtModel)</returns>
        public string Build()
        {
            var conditionDesc = string.Join(" ", _words);
            foreach (var condition in _conditions)
            {
                if (condition.Item1.Match(conditionDesc).Success)
                    return condition.Item2;
            }
            throw new InvalidSemanticItemException(null, conditionDesc);
        }
    }
}
