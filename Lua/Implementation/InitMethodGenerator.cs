using ProfitRobots.StrategyGenerator.Model;
using System;
using System.Globalization;
using System.Linq;

namespace ProfitRobots.StrategyGenerator.Lua.Implementation
{
    class InitMethodGenerator
    {
        MetaModel _model;

        private InitMethodGenerator(MetaModel model)
        {
            _model = model;
        }

        /// <summary>
        /// Adds init method code.
        /// </summary>
        /// <param name="strategyModel">Model</param>
        /// <exception cref="StrategyGeneratorException">When the model is invalid</exception>
        public static IPrimitive GenerateCode(MetaModel strategyModel)
        {
            return new InitMethodGenerator(strategyModel).DoAddCode();
        }

        IPrimitive DoAddCode()
        {
            return new CodeBlock()
                .Add("function Init()")
                .Add(string.Format("    strategy:name(\"{0}\");", _model.Name ?? ""))
                .Add(string.Format("    strategy:description(\"{0}\");", _model.Description ?? ""))
                .Add(GenerateUserParameters())
                .Add(GenerateActionParameters())
                .Add(GenerateModulesParameters())
                .Add("end");
        }

        private IPrimitive GenerateUserParameters()
        {
            var code = new CodeBlock();
            foreach (var parameter in _model.Parameters)
            {
                switch (parameter)
                {
                    case IntParameter intParam:
                        code.Add($"    strategy.parameters:addInteger(\"{intParam.Id}\", \"{intParam.Name ?? ""}\", \"{intParam.Description ?? ""}\", {intParam.Value});");
                        break;
                    case PriceTypeParameter priceTypeParameter:
                        string priceType = priceTypeParameter.Value == PriceType.Bid ? "true" : "false";
                        code.Add($"    strategy.parameters:addBoolean(\"{priceTypeParameter.Id}\", \"{priceTypeParameter.Name ?? ""}\", \"{priceTypeParameter.Description ?? ""}\", {priceType});");
                        code.Add($"    strategy.parameters:setFlag(\"{priceTypeParameter.Id}\", core.FLAG_BIDASK);");
                        break;
                    case PriceParameter priceParameter:
                        code.Add($"    strategy.parameters:addDouble(\"{priceParameter.Id}\", \"{priceParameter.Name ?? ""}\", \"{priceParameter.Description ?? ""}\", {priceParameter.Value.ToLuaDoubleString()});");
                        code.Add($"    strategy.parameters:setFlag(\"{priceParameter.Id}\", core.FLAG_PRICE);");
                        break;
                    case DoubleParameter doubleParameter:
                        code.Add($"    strategy.parameters:addDouble(\"{doubleParameter.Id}\", \"{doubleParameter.Name ?? ""}\", \"{doubleParameter.Description ?? ""}\", {doubleParameter.Value.ToLuaDoubleString()});");
                        break;
                    case TimeframeParameter timeframeParameter:
                        code.Add($"    strategy.parameters:addString(\"{timeframeParameter.Id}\", \"{timeframeParameter.Name ?? ""}\", \"{timeframeParameter.Description ?? ""}\", \"{timeframeParameter.Value ?? ""}\");");
                        code.Add($"    strategy.parameters:setFlag(\"{timeframeParameter.Id}\", core.FLAG_PERIODS);");
                        break;
                    case StringParameter stringParameter:
                        code.Add($"    strategy.parameters:addString(\"{stringParameter.Id}\", \"{stringParameter.Name ?? ""}\", \"{stringParameter.Description ?? ""}\", \"{stringParameter.Value ?? ""}\");");
                        break;
                    case BoolParameter boolParameter:
                        var boolVal = boolParameter.Value ? "true" : "false";
                        code.Add($"    strategy.parameters:addBoolean(\"{boolParameter.Id}\", \"{boolParameter.Name ?? ""}\", \"{boolParameter.Description ?? ""}\", {boolVal});");
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            return code;
        }

        private IPrimitive GenerateActionParameters()
        {
            var code = new CodeBlock();
            foreach (var action in _model.Actions.Where(a => a.ActionType == MetaActionType.Customizable))
            {
                code.Add($"    strategy.parameters:addString(\"{action.ActionId}\", \"Action on {action.Name}\", \"\", \"none\");")
                    .Add($"    strategy.parameters:addStringAlternative(\"{action.ActionId}\", \"None\", \"\", \"none\");")
                    .Add($"    strategy.parameters:addStringAlternative(\"{action.ActionId}\", \"Buy\", \"\", \"buy\");")
                    .Add($"    strategy.parameters:addStringAlternative(\"{action.ActionId}\", \"Sell\", \"\", \"sell\");")
                    .Add($"    strategy.parameters:addStringAlternative(\"{action.ActionId}\", \"Exit\", \"\", \"exit\");");
            }
            return code;
        }

        private IPrimitive GenerateModulesParameters()
        {
            var code = new CodeBlock();
            foreach (var module in _model.Modules)
            {
                code.Add($"    {module.Name}:Init(strategy.parameters);");
            }
            return code;
        }
    }
}
