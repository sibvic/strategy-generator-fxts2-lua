using ProfitRobots.StrategyGenerator.Lua.Implementation.Primitives;
using ProfitRobots.StrategyGenerator.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfitRobots.StrategyGenerator.Lua.Implementation
{
    /// <summary>
    /// Generates code of the entry function
    /// </summary>
    class EntryFunctionGenerator
    {
        VariablesStorage _variables = new VariablesStorage();
        MetaModel _model;
        PrimitiveFactory _factory;

        public EntryFunctionGenerator(MetaModel model, PrimitiveFactory factory)
        {
            _model = model;
            _factory = factory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="StrategyGeneratorException">On missing data</exception>
        /// <returns></returns>
        public IPrimitive Generate()
        {
            var code = new CodeBlock()
                .Add("function EntryFunction(source, period)")
                .Add(GenerateIndicators())
                .Add(GenerateActions());
            if (_model.Debug)
            {
                code.Add("    debug_helper:Next();");
            }
            code.Add("end")
                .Add(GenerateDoActionFunction());
            return code;
        }

        private List<IPrimitive> GenerateIndicators()
        {
            return _model.Indicators.Select(indicator => $"    {indicator.VariableName}:update(core.UpdateLast);".MakePrimitive()).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="StrategyGeneratorException"></exception>
        /// <returns></returns>
        private List<IPrimitive> GenerateActions()
        {
            return _model.Actions
                .Select(a => new IfStatement(new ConditionFormatter(_factory).FormatCondition(a.Condition), FormatAction(a), _variables, "    ") as IPrimitive)
                .ToList();
        }

        private IPrimitive GenerateDoActionFunction()
        {
            var code = new CodeBlock();
            if (_model.Actions.Exists(a => a.ActionType == MetaActionType.Customizable))
            {
                code.Add("function DoAction(instrument, action)")
                    .Add("    if action == \"buy\" then")
                    .Add("        return trading:MarketOrder(source:instrument()):SetSide(\"B\"):SetDefaultAmount():Execute();")
                    .Add("    elseif action == \"sell\" then")
                    .Add("        return trading:MarketOrder(source:instrument()):SetSide(\"S\"):SetDefaultAmount():Execute();")
                    .Add("    elseif action == \"exit\" then")
                    .Add("        return trading:CloseAllForInstrument(instrument);")
                    .Add("    end")
                    .Add("    return false;")
                    .Add("end");
            }
            return code;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <exception cref="StrategyGeneratorException"></exception>
        /// <returns></returns>
        private IPrimitive FormatAction(MetaAction action)
        {
            switch (action.ActionType)
            {
                case MetaActionType.Buy:
                    return CreateMarketOrder(action, "B");
                case MetaActionType.Sell:
                    return CreateMarketOrder(action, "S");
                case MetaActionType.Customizable:
                    return $"DoAction(source:instrument(), instance.parameters.{action.ActionId});".MakePrimitive();
                case MetaActionType.Exit:
                    return "trading:CloseAllForInstrument(source:instrument());".MakePrimitive();
                case MetaActionType.ExitBuy:
                    return "trading:CloseSideForInstrument(source:instrument(), \"B\");".MakePrimitive();
                case MetaActionType.ExitSell:
                    return "trading:CloseSideForInstrument(source:instrument(), \"S\");".MakePrimitive();
                case MetaActionType.EntryBuy:
                    return CreateEntryOrder(action, "B");
                case MetaActionType.EntrySell:
                    return CreateEntryOrder(action, "S");
            }
            throw new NotSupportedActionException(action.ActionType, "Lua");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="side"></param>
        /// <exception cref="StrategyGeneratorException">On missing rate for the entry</exception>
        /// <returns></returns>
        private IPrimitive CreateEntryOrder(MetaAction action, string side)
        {
            if (action.Entry == null)
                throw new StrategyGeneratorException($"No entry rate for the {action.Name}");

            (string value, bool isValueInPips) = FormatOrderValue(action.Entry);
            string entry = $":SetRate({value})";

            var code = new CodeBlock();
            code.Add($"local command = trading:EntryOrder(source:instrument()):SetSide(\"{side}\"):SetDefaultAmount(){entry}:SetCustomID(custom_id);");
            AddStop(action, code);
            AddLimit(action, code);
            code.Add("command:Execute();");
            return code;
        }

        private void AddLimit(MetaAction action, CodeBlock code)
        {
            (string limitValue, bool isLimitValueInPips) = FormatOrderValue(action.Limit);
            if (limitValue != null)
            {
                if (isLimitValueInPips)
                {
                    code.Add($"command = command:SetPipLimit({limitValue});");
                }
                else
                {
                    code.Add($"command = command:SetLimit({limitValue});");
                }
            }
            else
            {
                code.Add(new IfStatement("instance.parameters.set_limit".MakePrimitive(),
                    "command = command:SetPipLimit(nil, instance.parameters.limit);".MakePrimitive(),
                    _variables, ""));
            }
        }

        private void AddStop(MetaAction action, CodeBlock code)
        {
            (string stopValue, bool isStopValueInPips) = FormatOrderValue(action.Stop);
            if (stopValue != null)
            {
                if (isStopValueInPips)
                {
                    code.Add($"command = command:SetPipStop({stopValue});");
                }
                else
                {
                    code.Add($"command = command:SetStop({stopValue});");
                }
            }
            else
            {
                code.Add(new IfStatement("instance.parameters.set_stop".MakePrimitive(),
                    "command = command:SetPipStop(nil, instance.parameters.stop, instance.parameters.trailing_stop and instance.parameters.trailing or nil);".MakePrimitive(),
                    _variables, ""));
            }
        }

        private IPrimitive CreateMarketOrder(MetaAction action, string side)
        {
            var code = new CodeBlock();
            code.Add("local current_serial = source:serial(period);");

            var executeCommandCode = new CodeBlock();
            executeCommandCode.Add("last_serial = current_serial;");

            var oppositeSide = side == "B" ? "S" : "B";
            var closeTradesCode = new CodeBlock();
            closeTradesCode
                .Add($"local trades = trading:FindTrade():WhenSide(\"{oppositeSide}\"):WhenCustomID(custom_id):All();")
                .Add("for _, trade in ipairs(trades) do")
                .Add("    trading:Close(trade);")
                .Add("end");
            executeCommandCode.Add(PrimitiveBuilder
                .If("instance.parameters.close_on_opposite")
                .Then(closeTradesCode)
                .Build(_variables));

            executeCommandCode.Add($"local command = trading:MarketOrder(source:instrument()):SetSide(\"{side}\"):SetDefaultAmount():SetCustomID(custom_id);");
            AddStop(action, executeCommandCode);
            AddLimit(action, executeCommandCode);
            executeCommandCode.Add("command:Execute();");

            code.Add(new IfStatement(new NotEqualPrimitive("last_serial", "current_serial"),
                executeCommandCode, _variables, ""));
            
            return code;
        }

        private static (string value, bool isValueInPips) FormatOrderValue(MetaOrder order)
        {
            if (order == null)
            {
                return (null, false);
            }
            StringBuilder formula = new StringBuilder();
            bool valueInPips = true;
            string lastOperation = null;
            foreach (var value in order.ValueStack)
            {
                switch (value.ValueType)
                {
                    case FormulaItemType.Operand:
                        formula.Append(value.Value);
                        lastOperation = value.Value;
                        break;
                    case FormulaItemType.Parameter:
                        switch (lastOperation)
                        {
                            case "-":
                            case "+":
                                formula.Append($"instance.parameters.{value.Value}*source:pipSize()");
                                break;
                            default:
                                formula.Append($"instance.parameters.{value.Value}");
                                break;
                        }
                        break;
                    case FormulaItemType.Stream:
                    case FormulaItemType.StreamValue:
                        if (string.IsNullOrEmpty(value.Substream))
                        {
                            formula.Append($"{value.Value}:tick(period)");
                        }
                        else
                        {
                            if (value.Value == null && value.StreamType == StreamType.Instrument)
                                formula.Append($"source.{value.Substream}:tick(period)");
                            else
                                formula.Append($"{value.Value}.{value.Substream}:tick(period)");
                        }
                        valueInPips = false;
                        break;
                    case FormulaItemType.Value:
                        switch (lastOperation)
                        {
                            case "-":
                            case "+":
                                formula.Append($"{value.Value}*source:pipSize()");
                                break;
                            default:
                                formula.Append($"{value.Value}");
                                break;
                        }
                        break;
                }
            }
            return (formula.ToString(), valueInPips);
        }
    }
}
