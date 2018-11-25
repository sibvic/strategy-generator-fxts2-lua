using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProfitRobots.StrategyGenerator.Model;

namespace ProfitRobots.StrategyGenerator.ModelParser.Tests
{
    [TestClass]
    public class ConditionParserTests
    {
        [TestCategory("Condition parser")]
        [TestMethod]
        public void ModelParser_CloseCrossUnderMVA()
        {
            var model = new StrategyModel()
            {
                Sources = new System.Collections.Generic.List<Source>()
                {
                    new Source()
                    {
                        Id = "mva",
                        Name = "MVA",
                        SourceType = Source.INDICATOR
                    }
                }
            };

            var parser = new ConditionParser(model);
            var condition = parser.Parse("close cross over mva");
            Assert.IsNotNull(condition);
            Assert.AreEqual(Condition.CROSSES_OVER, condition.ConditionType);
            Assert.AreEqual(null, condition.Arg1.Value);
            Assert.AreEqual(FormulaItem.STREAM, condition.Arg1.ValueType);
            Assert.AreEqual("close", condition.Arg1.Substream);
            Assert.AreEqual(FormulaItem.STREAM, condition.Arg2.ValueType);
            Assert.AreEqual("mva", condition.Arg2.Value);
            Assert.IsNotNull(condition.Conditions);
            Assert.AreEqual("close cross over mva", condition.UserInput);
        }

        [TestCategory("Condition parser")]
        [TestMethod]
        public void ModelParser_CloseCrossMVA()
        {
            var model = new StrategyModel()
            {
                Sources = new System.Collections.Generic.List<Source>()
                {
                    new Source()
                    {
                        Id = "mva",
                        Name = "MVA",
                        SourceType = Source.INDICATOR
                    }
                }
            };

            var parser = new ConditionParser(model);
            var condition = parser.Parse("close cross mva");
            Assert.IsNotNull(condition);
            Assert.AreEqual(Condition.CROSSES, condition.ConditionType);
            Assert.AreEqual(null, condition.Arg1.Value);
            Assert.AreEqual(FormulaItem.STREAM, condition.Arg1.ValueType);
            Assert.AreEqual("close", condition.Arg1.Substream);
            Assert.AreEqual(FormulaItem.STREAM, condition.Arg2.ValueType);
            Assert.AreEqual("mva", condition.Arg2.Value);
            Assert.IsNotNull(condition.Conditions);
            Assert.AreEqual("close cross mva", condition.UserInput);
        }

        [TestCategory("Condition parser")]
        [TestMethod]
        public void ModelParser_CloseCrossMVAWithShift()
        {
            var model = new StrategyModel()
            {
                Sources = new System.Collections.Generic.List<Source>()
                {
                    new Source()
                    {
                        Id = "mva",
                        Name = "MVA",
                        SourceType = Source.INDICATOR
                    }
                }
            };

            var parser = new ConditionParser(model);
            var condition = parser.Parse("close cross mva 2 period back");
            Assert.IsNotNull(condition);
            Assert.AreEqual(Condition.CROSSES, condition.ConditionType);
            Assert.AreEqual(null, condition.Arg1.Value);
            Assert.AreEqual(FormulaItem.STREAM, condition.Arg1.ValueType);
            Assert.AreEqual(2, condition.Arg1.PeriodShift);
            Assert.AreEqual("close", condition.Arg1.Substream);
            Assert.AreEqual(FormulaItem.STREAM, condition.Arg2.ValueType);
            Assert.AreEqual(2, condition.Arg2.PeriodShift);
            Assert.AreEqual("mva", condition.Arg2.Value);
            Assert.IsNotNull(condition.Conditions);
            Assert.AreEqual("close cross mva 2 period back", condition.UserInput);
        }

        [TestCategory("Condition parser")]
        [TestMethod]
        public void ModelParser_CloseCrossesMVA()
        {
            var model = new StrategyModel()
            {
                Sources = new System.Collections.Generic.List<Source>()
                {
                    new Source()
                    {
                        Id = "mva",
                        Name = "MVA",
                        SourceType = Source.INDICATOR
                    }
                }
            };

            var parser = new ConditionParser(model);
            var condition = parser.Parse("close crosses mva");
            Assert.IsNotNull(condition);
            Assert.AreEqual(Condition.CROSSES, condition.ConditionType);
            Assert.AreEqual(null, condition.Arg1.Value);
            Assert.AreEqual(FormulaItem.STREAM, condition.Arg1.ValueType);
            Assert.AreEqual("close", condition.Arg1.Substream);
            Assert.AreEqual(FormulaItem.STREAM, condition.Arg2.ValueType);
            Assert.AreEqual("mva", condition.Arg2.Value);
            Assert.IsNotNull(condition.Conditions);
            Assert.AreEqual("close crosses mva", condition.UserInput);
        }

        [TestCategory("Condition parser")]
        [TestMethod]
        public void ModelParser_IndicatorLessThanNumber()
        {
            var model = new StrategyModel()
            {
                Sources = new System.Collections.Generic.List<Source>()
                {
                    new Source()
                    {
                        Id = "rsi",
                        Name = "RSI",
                        SourceType = Source.INDICATOR
                    }
                }
            };

            var parser = new ConditionParser(model);
            var condition = parser.Parse("rsi < 20");
            Assert.IsNotNull(condition);
            Assert.AreEqual(Condition.LT, condition.ConditionType);
            Assert.AreEqual("rsi", condition.Arg1.Value);
            Assert.AreEqual(FormulaItem.STREAM, condition.Arg1.ValueType);
            Assert.AreEqual(FormulaItem.VALUE, condition.Arg2.ValueType);
            Assert.AreEqual("20.0", condition.Arg2.Value);
            Assert.AreEqual("rsi < 20", condition.UserInput);
        }

        [TestCategory("Condition parser")]
        [TestMethod]
        public void ModelParser_IndicatorSubstream()
        {
            var model = new StrategyModel()
            {
                Sources = new System.Collections.Generic.List<Source>()
                {
                    new Source()
                    {
                        Id = "stoch",
                        Name = "STOCHASTIC",
                        SourceType = Source.INDICATOR
                    }
                }
            };

            var parser = new ConditionParser(model);
            var condition = parser.Parse("stoch.K <= 30");
            Assert.IsNotNull(condition);
            Assert.AreEqual(Condition.LTE, condition.ConditionType);
            Assert.AreEqual("stoch", condition.Arg1.Value);
            Assert.AreEqual("K", condition.Arg1.Substream);
            Assert.AreEqual(FormulaItem.STREAM, condition.Arg1.ValueType);
            Assert.AreEqual(FormulaItem.VALUE, condition.Arg2.ValueType);
            Assert.AreEqual("30.0", condition.Arg2.Value);
            Assert.AreEqual("stoch.K <= 30", condition.UserInput);
        }

        [TestCategory("Condition parser")]
        [TestMethod]
        [ExpectedException(typeof(InvalidSemanticItemException))]
        public void ModelParser_IncorrectIndicatorSubstream()
        {
            var model = new StrategyModel()
            {
                Sources = new System.Collections.Generic.List<Source>()
                {
                    new Source()
                    {
                        Id = "stoch",
                        Name = "STOCHASTIC",
                        SourceType = Source.INDICATOR
                    }
                }
            };

            var parser = new ConditionParser(model);
            parser.Parse("stoch1.K <= 30");
        }
        
        [TestCategory("Condition parser")]
        [TestMethod]
        public void ModelParser_BoolParmeterInCondition()
        {
            var model = new StrategyModel()
            {
                Sources = new System.Collections.Generic.List<Source>()
                {
                    new Source()
                    {
                        Id = "stoch",
                        Name = "STOCHASTIC",
                        SourceType = Source.INDICATOR
                    }
                },
                Parameters = new System.Collections.Generic.List<StrategyParameter>()
                {
                    new StrategyParameter()
                    {
                        Id = "allow_buy",
                        ParameterType = StrategyParameter.BOOLEAN,
                        DefaultValue = "true"
                    }
                }
            };

            var parser = new ConditionParser(model);
            var condition = parser.Parse("stoch.K > 30 and allow_buy");
            Assert.IsNotNull(condition);

            Assert.AreEqual(Condition.AND, condition.ConditionType);
            Assert.AreEqual(2, condition.Conditions.Count);

            Assert.AreEqual(Condition.GT, condition.Conditions[0].ConditionType);
            Assert.AreEqual("stoch", condition.Conditions[0].Arg1.Value);
            Assert.AreEqual("K", condition.Conditions[0].Arg1.Substream);
            Assert.AreEqual(FormulaItem.STREAM, condition.Conditions[0].Arg1.ValueType);
            Assert.AreEqual(FormulaItem.VALUE, condition.Conditions[0].Arg2.ValueType);
            Assert.AreEqual("30.0", condition.Conditions[0].Arg2.Value);

            Assert.AreEqual(Condition.EQ, condition.Conditions[1].ConditionType);
            Assert.AreEqual("allow_buy", condition.Conditions[1].Arg1.Value);
            Assert.AreEqual(FormulaItem.PARAM, condition.Conditions[1].Arg1.ValueType);
            Assert.AreEqual(FormulaItem.VALUE, condition.Conditions[1].Arg2.ValueType);
            Assert.AreEqual("true", condition.Conditions[1].Arg2.Value);

            Assert.AreEqual("stoch.K > 30 and allow_buy", condition.UserInput);
        }

        [TestCategory("Condition parser")]
        [TestMethod]
        public void ModelParser_AndCondition()
        {
            var model = new StrategyModel()
            {
                Sources = new System.Collections.Generic.List<Source>()
                {
                    new Source()
                    {
                        Id = "stoch",
                        Name = "STOCHASTIC",
                        SourceType = Source.INDICATOR
                    },
                    new Source()
                    {
                        Id = "rsi",
                        Name = "RSI",
                        SourceType = Source.INDICATOR
                    }
                }
            };

            var parser = new ConditionParser(model);
            var condition = parser.Parse("stoch.K == 30 and rsi != 20");
            Assert.IsNotNull(condition);

            Assert.AreEqual(Condition.AND, condition.ConditionType);
            Assert.AreEqual(2, condition.Conditions.Count);

            Assert.AreEqual(Condition.EQ, condition.Conditions[0].ConditionType);
            Assert.AreEqual("stoch", condition.Conditions[0].Arg1.Value);
            Assert.AreEqual("K", condition.Conditions[0].Arg1.Substream);
            Assert.AreEqual(FormulaItem.STREAM, condition.Conditions[0].Arg1.ValueType);
            Assert.AreEqual(FormulaItem.VALUE, condition.Conditions[0].Arg2.ValueType);
            Assert.AreEqual("30.0", condition.Conditions[0].Arg2.Value);

            Assert.AreEqual(Condition.NEQ, condition.Conditions[1].ConditionType);
            Assert.AreEqual("rsi", condition.Conditions[1].Arg1.Value);
            Assert.AreEqual(FormulaItem.STREAM, condition.Conditions[1].Arg1.ValueType);
            Assert.AreEqual(FormulaItem.VALUE, condition.Conditions[1].Arg2.ValueType);
            Assert.AreEqual("20.0", condition.Conditions[1].Arg2.Value);

            Assert.AreEqual("stoch.K == 30 and rsi != 20", condition.UserInput);
        }
    }
}
