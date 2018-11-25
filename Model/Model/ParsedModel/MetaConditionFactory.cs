using System;
using System.Collections.Generic;
using System.Linq;

namespace ProfitRobots.StrategyGenerator.Model.ModelParser
{
    public class MetaConditionFactory
    {
        public static ICondition Create(Condition condition)
        {
            switch (condition.ConditionType)
            {
                case Condition.AND:
                    return new MultiArgumentCondition()
                    {
                        ConditionType = ConditionType.And,
                        Subconditions = CreateSubconditions(condition.Conditions)
                    };
                case Condition.OR:
                    return new MultiArgumentCondition()
                    {
                        ConditionType = ConditionType.Or,
                        Subconditions = CreateSubconditions(condition.Conditions)
                    };
                case Condition.CROSSES:
                    return new TwoArgumentCondition()
                    {
                        ConditionType = ConditionType.Crosses,
                        Arg1 = MetaFormulaItem.Create(condition.Arg1),
                        Arg2 = MetaFormulaItem.Create(condition.Arg2)
                    };
                case Condition.CROSSES_OVER:
                    return new TwoArgumentCondition()
                    {
                        ConditionType = ConditionType.CrossesOver,
                        Arg1 = MetaFormulaItem.Create(condition.Arg1),
                        Arg2 = MetaFormulaItem.Create(condition.Arg2)
                    };
                case Condition.CROSSES_OVER_OR_TOUCH:
                    return new TwoArgumentCondition()
                    {
                        ConditionType = ConditionType.CrossesOverOrTouch,
                        Arg1 = MetaFormulaItem.Create(condition.Arg1),
                        Arg2 = MetaFormulaItem.Create(condition.Arg2)
                    };
                case Condition.CROSSES_UNDER:
                    return new TwoArgumentCondition()
                    {
                        ConditionType = ConditionType.CrossesUnder,
                        Arg1 = MetaFormulaItem.Create(condition.Arg1),
                        Arg2 = MetaFormulaItem.Create(condition.Arg2)
                    };
                case Condition.CROSSES_UNDER_OR_TOUCH:
                    return new TwoArgumentCondition()
                    {
                        ConditionType = ConditionType.CrossesUnderOrTouch,
                        Arg1 = MetaFormulaItem.Create(condition.Arg1),
                        Arg2 = MetaFormulaItem.Create(condition.Arg2)
                    };
                case Condition.GT:
                    return new TwoArgumentCondition()
                    {
                        ConditionType = ConditionType.Greater,
                        Arg1 = MetaFormulaItem.Create(condition.Arg1),
                        Arg2 = MetaFormulaItem.Create(condition.Arg2)
                    };
                case Condition.LT:
                    return new TwoArgumentCondition()
                    {
                        ConditionType = ConditionType.Lesser,
                        Arg1 = MetaFormulaItem.Create(condition.Arg1),
                        Arg2 = MetaFormulaItem.Create(condition.Arg2)
                    };
                case Condition.GTE:
                    return new TwoArgumentCondition()
                    {
                        ConditionType = ConditionType.GreaterOrEqual,
                        Arg1 = MetaFormulaItem.Create(condition.Arg1),
                        Arg2 = MetaFormulaItem.Create(condition.Arg2)
                    };
                case Condition.LTE:
                    return new TwoArgumentCondition()
                    {
                        ConditionType = ConditionType.LesserOrEqual,
                        Arg1 = MetaFormulaItem.Create(condition.Arg1),
                        Arg2 = MetaFormulaItem.Create(condition.Arg2)
                    };
                case Condition.EQ:
                    return new TwoArgumentCondition()
                    {
                        ConditionType = ConditionType.Equal,
                        Arg1 = MetaFormulaItem.Create(condition.Arg1),
                        Arg2 = MetaFormulaItem.Create(condition.Arg2)
                    };
                case Condition.NEQ:
                    return new TwoArgumentCondition()
                    {
                        ConditionType = ConditionType.NotEqual,
                        Arg1 = MetaFormulaItem.Create(condition.Arg1),
                        Arg2 = MetaFormulaItem.Create(condition.Arg2)
                    };
                default:
                    throw new NotImplementedException();
            }
        }

        private static List<ICondition> CreateSubconditions(List<Condition> subconditions)
        {
            return subconditions.Select(a => Create(a)).ToList();
        }
    }
}
