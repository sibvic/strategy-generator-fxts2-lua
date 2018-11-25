using ProfitRobots.StrategyGenerator.Model;

namespace ProfitRobots.StrategyGenerator.Lua.Implementation
{
    class ConditionFormatter
    {
        PrimitiveFactory _factory;
        public ConditionFormatter(PrimitiveFactory factory)
        {
            _factory = factory;
        }

        public IPrimitive FormatCondition(ICondition condition)
        {
            var twoArgumentCondition = condition as TwoArgumentCondition;
            switch (condition.ConditionType)
            {
                case ConditionType.Crosses:
                    return FormatCrossMethodCall(twoArgumentCondition.Arg1, twoArgumentCondition.Arg2, "core.crosses");
                case ConditionType.CrossesOver:
                    return FormatCrossMethodCall(twoArgumentCondition.Arg1, twoArgumentCondition.Arg2, "core.crossesOver");
                case ConditionType.CrossesOverOrTouch:
                    return FormatCrossMethodCall(twoArgumentCondition.Arg1, twoArgumentCondition.Arg2, "core.crossesOverOrTouch");
                case ConditionType.CrossesUnder:
                    return FormatCrossMethodCall(twoArgumentCondition.Arg1, twoArgumentCondition.Arg2, "core.crossesUnder");
                case ConditionType.CrossesUnderOrTouch:
                    return FormatCrossMethodCall(twoArgumentCondition.Arg1, twoArgumentCondition.Arg2, "core.crossesUnderOrTouch");
                case ConditionType.Greater:
                    return FormatComparison(twoArgumentCondition.Arg1, twoArgumentCondition.Arg2, ">");
                case ConditionType.Lesser:
                    return FormatComparison(twoArgumentCondition.Arg1, twoArgumentCondition.Arg2, "<");
                case ConditionType.GreaterOrEqual:
                    return FormatComparison(twoArgumentCondition.Arg1, twoArgumentCondition.Arg2, ">=");
                case ConditionType.LesserOrEqual:
                    return FormatComparison(twoArgumentCondition.Arg1, twoArgumentCondition.Arg2, "<=");
                case ConditionType.Equal:
                    return FormatComparison(twoArgumentCondition.Arg1, twoArgumentCondition.Arg2, "==");
                case ConditionType.NotEqual:
                    return FormatComparison(twoArgumentCondition.Arg1, twoArgumentCondition.Arg2, "~=");
                case ConditionType.And:
                    {
                        var multiArgumentCondition = condition as MultiArgumentCondition;
                        var and = new AndPrimitive();
                        foreach (var subcondition in multiArgumentCondition.Subconditions)
                        {
                            and.AddParameter(FormatCondition(subcondition));
                        }
                        return _factory.MakeConst(and);
                    }
                case ConditionType.Or:
                    {
                        var multiArgumentCondition = condition as MultiArgumentCondition;
                        var or = new OrPrimitive();
                        foreach (var subcondition in multiArgumentCondition.Subconditions)
                        {
                            or.AddParameter(FormatCondition(subcondition));
                        }
                        return _factory.MakeConst(or);
                    }
            }
            throw new NotSupportedConditionException(condition.ConditionType, "Lua");
        }

        private IPrimitive FormatComparison(MetaFormulaItem arg1, MetaFormulaItem arg2, string v)
        {
            return _factory.MakeConst(new ComparePrimitive(FormatStreamValueOrValue(arg1), v, FormatStreamValueOrValue(arg2)));
        }

        private static IPrimitive AddRangeChecks(MetaFormulaItem arg1, MetaFormulaItem arg2, IPrimitive crossCheck)
        {
            return new AndPrimitive()
                .AddParameter(FormatRangeCheck(arg1))
                .AddParameter(FormatRangeCheck(arg2))
                .AddParameter(crossCheck);
        }

        /// <summary>
        /// Formats range check
        /// </summary>
        /// <param name="item">Item to format the range check to.</param>
        /// <exception cref="NotSupportedFormulaItemException"></exception>
        /// <returns>Primitive with formatted range check or null when there is no need to check it.</returns>
        private static IPrimitive FormatRangeCheck(MetaFormulaItem item)
        {
            switch (item.ValueType)
            {
                case FormulaItemType.Stream:
                    {
                        var minCount = item.Value == "source" ? "3" : "2";
                        var streamSize = FormatStreamName(item) + ":size()";
                        var streamMinSize = string.IsNullOrEmpty(item.PeriodShift) ? minCount : $"{minCount} + {item.PeriodShift}";
                        return $"{streamSize} >= {streamMinSize}".MakePrimitive();
                    }
                case FormulaItemType.StreamValue:
                    {
                        var minCount = item.Value == "source" ? "2" : "1";
                        var streamSize = FormatStreamName(item) + ":size()";
                        var streamMinSize = string.IsNullOrEmpty(item.PeriodShift) ? minCount : $"{minCount} + {item.PeriodShift}";
                        return $"{streamSize} >= {streamMinSize}".MakePrimitive();
                    }
                case FormulaItemType.Parameter:
                case FormulaItemType.Value:
                    return null;
                default:
                    throw new NotSupportedFormulaItemException(item.ValueType, "Lua");
            }
        }

        private IPrimitive FormatStreamOrValue(MetaFormulaItem arg)
        {
            switch (arg.ValueType)
            {
                case FormulaItemType.Stream:
                    return FormatStreamName(arg).MakePrimitive();
                default:
                    return FormatStreamValueOrValue(arg);
            }
        }

        /// <summary>
        /// Formats a value
        /// </summary>
        /// <param name="item">Item to format.</param>
        /// <exception cref="NotSupportedFormulaItemException">When formula of the specified type is not supported</exception>
        /// <returns>Primitive with the formatted value</returns>
        private IPrimitive FormatStreamValueOrValue(MetaFormulaItem item)
        {
            switch (item.ValueType)
            {
                case FormulaItemType.Value:
                    return item.Value.MakePrimitive();
                case FormulaItemType.Stream:
                case FormulaItemType.StreamValue:
                    if (item.PeriodShiftSource != null)
                    {
                        return new FunctionCall(FormatStreamName(item) + ":tick")
                            .AddParameter(FormatPeriod(item));
                    }
                    IPrimitive period = FormatNowPeriod(item);
                    return new FunctionCall(FormatStreamName(item) + ":tick")
                        .AddParameter(period)
                        .AddCondition(_factory.MakeIsNotNull(period));
                case FormulaItemType.Parameter:
                    return $"instance.parameters.{item.Value}".MakePrimitive();
                default:
                    throw new NotSupportedFormulaItemException(item.ValueType, "Lua");
            }
        }

        private static string FormatStreamName(MetaFormulaItem arg)
        {
            var substream = arg.Substream;
            if (arg.StreamType == StreamType.Indicator)
            {
                if (string.IsNullOrEmpty(substream))
                    substream = "DATA";
            }
            return string.IsNullOrEmpty(substream)
                ? arg.Value
                : string.IsNullOrEmpty(arg.Value) ? $"source.{substream}" : $"{arg.Value}.{substream}";
        }

        private IPrimitive FormatPeriod(MetaFormulaItem arg)
        {
            IPrimitive nowPeriod = FormatNowPeriod(arg);
            var getDateCall = new FunctionCall(arg.PeriodShiftSource + ":date")
                .AddParameter(nowPeriod)
                .AddCondition(_factory.MakeIsNotNull(nowPeriod));
            var findDateCall = new FunctionCall("core.findDate")
                .AddParameter(FormatStreamOrValue(arg))
                .AddParameter(getDateCall)
                .AddParameter("false");

            return findDateCall;
        }

        private IPrimitive FormatNowPeriod(MetaFormulaItem arg)
        {
            if (string.IsNullOrEmpty(arg.PeriodShift))
                return _factory.MakeConst($"trading_logic:GetLastPeriod(period, source, {FormatStreamName(arg)})".MakePrimitive(), true);
            return _factory.MakeConst($"trading_logic:GetPeriod(period - {arg.PeriodShift}, source, {FormatStreamName(arg)})".MakePrimitive(), true);
        }

        private IPrimitive FormatCrossMethodCall(MetaFormulaItem arg1, MetaFormulaItem arg2, string methodName)
        {
            var methodCall = new FunctionCall(methodName)
                .AddParameter(FormatStreamOrValue(arg1))
                .AddParameter(FormatStreamOrValue(arg2));

            if (arg1.PeriodShiftSource != null)
            {
                methodCall.AddParameter(FormatPeriod(arg1));
            }
            else
            {
                IPrimitive period = FormatNowPeriod(arg1);
                methodCall.AddParameter(period)
                    .AddCondition(_factory.MakeIsNotNull(period));
            }
            if (arg2.ValueType == FormulaItemType.Stream)
            {
                if (arg2.PeriodShiftSource != null)
                {
                    methodCall.AddParameter(FormatPeriod(arg2));
                }
                else
                {
                    IPrimitive period = FormatNowPeriod(arg2);
                    methodCall.AddParameter(period)
                        .AddCondition(_factory.MakeIsNotNull(period));
                }
            }
            return _factory.MakeConst(AddRangeChecks(arg1, arg2, methodCall));
        }
    }
}
