using ProfitRobots.StrategyGenerator.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProfitRobots.StrategyGenerator.ModelParser
{
    public class MetaModelFactory
    {
        public static MetaModule Create(Module module)
        {
            var newModule = new MetaModule()
            {
                Name = module.Name
            };
            if (module.Parameters != null)
            {
                foreach (var param in module.Parameters)
                {
                    newModule.Parameters.Add(MetaParameterFactory.Create(param));
                }
            }
            return newModule;
        }

        /// <summary>
        /// Creates meta model based on the strategy model.
        /// </summary>
        /// <param name="model">Strategy model.</param>
        /// <returns>Model</returns>
        public static MetaModel Create(StrategyModel model)
        {
            return new MetaModelFactory(model).Create();
        }

        #region Implementation
        SourceIdGenerator sourceIdGenerator = new SourceIdGenerator();
        StrategyModel model;
        private MetaModelFactory(StrategyModel model)
        {
            this.model = model;
        }

        private MetaModel Create()
        {
            var sources = BuildSourcesList();
            var model = new MetaModel()
            {
                Name = this.model.Name,
                Description = this.model.Description,
                Modules = BuildModulesList(),
                Indicators = BuildIndicatorsList(sources),
                Actions = BuildActionList(),
                Parameters = BuildParametersList(),
                Sources = sources,
                Debug = this.model.Debug == "true"
            };
            if (model.Debug)
            {
                model.Modules.Add(new MetaModule()
                {
                    Name = "debug_helper"
                });
            }
            return model;
        }

        private List<MetaModule> BuildModulesList()
        {
            if (model.Modules == null)
                return new List<MetaModule>();
            return model.Modules.Select(m => Create(m)).ToList();
        }

        private List<IMetaSource> BuildSourcesList()
        {
            var list = new List<IMetaSource>();
            if (model.Sources == null)
            {
                return list;
            }

            foreach (var source in model.Sources)
            {
                list.Add(Create(source));
            }
            return list;
        }

        private List<IParameter> BuildParametersList()
        {
            if (model.Parameters == null)
            {
                return new List<IParameter>();
            }

            return model.Parameters.Select(p => MetaParameterFactory.Create(p)).ToList();
        }

        private List<MetaAction> BuildActionList()
        {
            var list = new List<MetaAction>();
            if (model.Actions == null)
                return list;
            int actionIndex = 0;
            foreach (var action in model.Actions.Where(a => a.Condition != null))
            {
                var metaAction = Create(action);
                metaAction.ActionId = $"action_{++actionIndex}";
                list.Add(metaAction);
            }
            return list;
        }

        private List<Indicator> BuildIndicatorsList(List<IMetaSource> sources)
        {
            var list = new List<Indicator>();
            if (model.Sources == null)
                return list;

            foreach (var source in sources)
            {
                AddSourceToList(list, source);
            }
            return list;
        }

        private void AddSourceToList(List<Indicator> list, IMetaSource source)
        {
            if (source.SourceType == SourceType.Indicator)
            {
                AddIndicatorToList(list, source as IndicatorSource);
            }
        }

        private void AddIndicatorToList(List<Indicator> list, IndicatorSource source)
        {
            list.Add(new Indicator()
            {
                Name = source.Name.ToUpper(),
                VariableName = source.Id ?? sourceIdGenerator.Generate(source)
            });
        }
        #endregion

        public IMetaSource Create(Source source)
        {
            if (source == null)
                return new MainInstrumentSource();

            switch (source.SourceType)
            {
                case Source.INDICATOR:
                    {
                        if (source.IndicatorSource == null)
                        {
                            var sourceBuilder = new SourceParser(model);
                            if (source.IndicatorSourceId != null)
                                source.IndicatorSource = sourceBuilder.Parse(source.IndicatorSourceId);
                            else
                            {
                                source.IndicatorSource = new FormulaItem()
                                {
                                    ValueType = FormulaItem.STREAM,
                                    StreamType = FormulaItem.INSTRUMENT
                                };
                            }
                        }
                        var newSource = new IndicatorSource()
                        {
                            Name = source.Name,
                            Id = source.Id,
                            Source = Create(source.IndicatorSource)
                        };
                        if (source.Parameters != null)
                        {
                            foreach (var param in source.Parameters)
                            {
                                newSource.Parameters.Add(MetaParameterFactory.Create(param));
                            }
                        }
                        return newSource;
                    }
                case Source.INSTRUMENT:
                    {
                        return new InstrumentSource()
                        {
                            Id = source.Id,
                            PriceType = ParsePriceType(source.PriceType),
                            Timeframe = source.Timeframe
                        };
                    }
                default:
                    throw new NotImplementedException();
            }
            
        }

        public static MetaFormulaItem Create(FormulaItem argument)
        {
            if (argument == null)
                return null;

            return new MetaFormulaItem(argument);
        }

        private static string ParsePriceType(string priceType)
        {
            switch (priceType)
            {
                case Source.BID:
                    return "true";
                case Source.ASK:
                    return "false";
                default:
                    return ReplaceParameter(priceType);
            }
            throw new NotImplementedException();
        }

        internal static string ReplaceParameter(string parameter)
        {
            if (parameter == null)
            {
                return null;
            }
            foreach (var stream in ValuesParser.GetParams(parameter))
            {
                var streamValue = string.Format("instance.parameters.{0}", stream.Substring(1, stream.Length - 2));
                parameter = parameter.Replace(stream, streamValue);
            }
            return parameter;
        }

        public MetaAction Create(Model.Action action)
        {
            return new MetaAction()
            {
                Name = action.Name,
                Condition = Model.ModelParser.MetaConditionFactory.Create(action.Condition),
                Stop = Create(action.Stop),
                Limit = Create(action.Limit),
                Entry = Create(action.Entry),
                ActionType = ParseActionType(action.ActionType)
            };
        }

        private static MetaActionType ParseActionType(string actionType)
        {
            switch (actionType)
            {
                case Model.Action.BUY:
                    return MetaActionType.Buy;
                case Model.Action.SELL:
                    return MetaActionType.Sell;
                case Model.Action.CUSTOM:
                    return MetaActionType.Customizable;
                case Model.Action.EXIT:
                    return MetaActionType.Exit;
                case Model.Action.EXIT_BUY:
                    return MetaActionType.ExitBuy;
                case Model.Action.EXIT_SELL:
                    return MetaActionType.ExitSell;
                case Model.Action.ENTRY_BUY:
                    return MetaActionType.EntryBuy;
                case Model.Action.ENTRY_SELL:
                    return MetaActionType.EntrySell;
            }
            throw new NotSupportedException();
        }

        public MetaOrder Create(Order order)
        {
            if (order == null)
                return null;

            if (order.ValueStack == null)
            {
                throw new NotSupportedException();
            }
            return new MetaOrder()
            {
                ValueStack = order.ValueStack.Select(i => Create(i)).ToList()
            };
        }
    }
}
