using ProfitRobots.StrategyGenerator.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProfitRobots.StrategyGenerator.Lua.Implementation
{
    /// <summary>
    /// Generates code for the Prepare method
    /// </summary>
    class PrepareMethodGenerator
    {
        MetaModel _model;
        private PrepareMethodGenerator(MetaModel strategyModel)
        {
            _model = strategyModel;
        }

        public static IPrimitive GenerateCode(MetaModel strategyModel)
        {
            return new PrepareMethodGenerator(strategyModel).Generate();
        }

        private IPrimitive Generate()
        {
            var code = new CodeBlock()
                .Add("function Prepare(name_only)")
                .Add("    for _, module in pairs(Modules) do module:Prepare(nameOnly); end")
                .Add("    instance:name(profile:id() .. \"(\" .. instance.bid:name() ..  \")\");")
                .Add("    if name_only then return ; end")
                .Add("    custom_id = profile:id() .. \"_\" .. instance.bid:name();");
            foreach (var indicatorProfile in _model.Indicators.Select(i => i.Name).Distinct())
            {
                code.Add($"    assert(core.indicators:findIndicator(\"{indicatorProfile}\") ~= nil, \"Please, download and install {indicatorProfile} indicator\");");
            }
            code.Add(AddCreateSourcesCode())
                .Add("    trading_logic.DoTrading = EntryFunction;")
                .Add("end");
            return code;
        }

        private List<IMetaSource> SortSources(List<IMetaSource> sources)
        {
            var result = new List<IMetaSource>();

            IMetaSource GetNextSource()
            {
                foreach (var source in sources)
                {
                    switch (source)
                    {
                        case IndicatorSource indicatorSource:
                            {
                                if (indicatorSource.Source.Value == null)
                                    return source;
                                if (result.Any(s => s.Id == indicatorSource.Source.Value))
                                    return source;
                            }
                            break;
                        default:
                            return source;
                    }
                }
                return null;
            }

            while (sources.Any())
            {
                var source = GetNextSource();
                sources.Remove(source);
                result.Add(source);
            }
            return result;
        }

        private List<IPrimitive> AddCreateSourcesCode()
        {
            var lines = new List<IPrimitive>();
            foreach (var source in SortSources(_model.Sources))
            {
                lines.AddRange(AddCreateSourcesCode(source, null));
            }
            return lines;
        }

        string FormatSubstream(MetaFormulaItem item)
        {
            var source = _model.Sources.Where(s => s.Id == item.Value).FirstOrDefault();
            if (source == null)
                return string.IsNullOrEmpty(item.Substream) ? "" : "." + item.Substream;
            if (source.SourceType == SourceType.Indicator)
                return string.IsNullOrEmpty(item.Substream) ? ".DATA" : item.Substream;
            return string.IsNullOrEmpty(item.Substream) ? "" : "." + item.Substream;
        }

        IPrimitive GetIndicatorSourceId(MetaFormulaItem source)
        {
            var substream = FormatSubstream(source);
            if (source.Value == null)
            {
                return $"trading_logic.MainSource{substream}".MakePrimitive();
            }
                
            return $"{source.Value}{substream}".MakePrimitive();
        }

        internal static string ReplaceStringParameter(string parameter)
        {
            if (parameter == null)
            {
                return null;
            }
            if (parameter.StartsWith("{") && parameter.EndsWith("}"))
            {
                return $"instance.parameters.{parameter.Substring(1, parameter.Length - 2)}";
            }
            return $"\"{parameter}\"";
        }

        private List<IPrimitive> AddCreateSourcesCode(IMetaSource source, string parentSourceId)
        {
            var lines = new List<IPrimitive>();
            var sourceId = source.Id;
            switch (source)
            {
                case IndicatorSource indicatorSource:
                    var createIndicatorCall = new FunctionCall("core.indicators:create")
                        .AddParameter(new QuotedStringPrimitive(indicatorSource.Name.ToUpper()))
                        .AddParameter(GetIndicatorSourceId(indicatorSource.Source));
                    AddParameters(indicatorSource, createIndicatorCall);
                    lines.Add(new AssigmentStatement("    " + sourceId, createIndicatorCall));
                    if (_model.Debug)
                    {
                        lines.Add(new FunctionCall("    debug_helper:AddIndicator", true).AddParameter(sourceId));
                    }
                    break;
                case MainInstrumentSource maintInstrumentSource:
                    sourceId = "trading_logic.MainSource";
                    break;
                case InstrumentSource instrumentSource:
                    if (parentSourceId == null)
                    {
                        parentSourceId = sourceId;
                    }
                    var extSubscribeCall = new FunctionCall("ExtSubscribe")
                        .AddParameter(sourceId + "_id")
                        .AddParameter("nil")
                        .AddParameter(ReplaceStringParameter(instrumentSource.Timeframe))
                        .AddParameter(instrumentSource.PriceType)
                        .AddParameter(new QuotedStringPrimitive("bar"));
                    lines.Add(new AssigmentStatement($"    {sourceId}", extSubscribeCall));
                    if (_model.Debug)
                    {
                        lines.Add(new FunctionCall("    debug_helper:AddInstrument", true).AddParameter(sourceId));
                    }
                    break;
            }
            return lines;
        }

        private static void AddParameters(IndicatorSource indicatorSource, FunctionCall createIndicatorCall)
        {
            foreach (var param in indicatorSource.Parameters)
            {
                switch (param)
                {
                    case BoolParameter boolParam:
                        createIndicatorCall.AddParameter(boolParam.Value ? "true" : "false");
                        break;
                    case IntParameter intParam:
                        createIndicatorCall.AddParameter(intParam.Value.ToString());
                        break;
                    case StringParameter stringParam:
                        createIndicatorCall.AddParameter($"\"{stringParam.Value}\"");
                        break;
                    case ExternalParameter externalParam:
                        createIndicatorCall.AddParameter($"instance.parameters.{externalParam.Value}");
                        break;
                    case DoubleParameter doubleParam:
                        createIndicatorCall.AddParameter(doubleParam.Value.ToLuaDoubleString());
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}
