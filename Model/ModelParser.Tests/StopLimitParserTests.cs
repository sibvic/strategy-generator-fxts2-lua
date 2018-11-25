using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProfitRobots.StrategyGenerator.Model;
using System.Collections.Generic;
using System.Linq;

namespace ProfitRobots.StrategyGenerator.ModelParser.Tests
{
    [TestClass]
    public class StopLimitParserTests
    {
        [TestCategory("Stop/limit parser")]
        [TestMethod]
        public void TestPipsValue()
        {
            var parser = new StopLimitParser(new StrategyModel());
            var order = parser.Parse("10");
            Assert.IsNotNull(order);
            Assert.AreEqual(1, order.ValueStack.Count());
            Assert.AreEqual("10.0", order.ValueStack.First().Value);
            Assert.AreEqual(FormulaItem.VALUE, order.ValueStack.First().ValueType);
            Assert.AreEqual("10", order.UserInput);

            var doubleOrder = parser.Parse("25,5");
            Assert.IsNotNull(doubleOrder);
            Assert.AreEqual(1, doubleOrder.ValueStack.Count());
            Assert.AreEqual("25.5", doubleOrder.ValueStack.First().Value);
            Assert.AreEqual(FormulaItem.VALUE, doubleOrder.ValueStack.First().ValueType);
            Assert.AreEqual("25,5", doubleOrder.UserInput);

            var doubleOrder2 = parser.Parse("25.5");
            Assert.IsNotNull(doubleOrder2);
            Assert.AreEqual(1, doubleOrder2.ValueStack.Count());
            Assert.AreEqual("25.5", doubleOrder2.ValueStack.First().Value);
            Assert.AreEqual(FormulaItem.VALUE, doubleOrder2.ValueStack.First().ValueType);
            Assert.AreEqual("25.5", doubleOrder2.UserInput);
        }

        [TestCategory("Stop/limit parser")]
        [TestMethod]
        public void TestStreamValue()
        {
            var model = new StrategyModel();
            var parser = new StopLimitParser(model);
            var order = parser.Parse("low");
            Assert.IsNotNull(order);
            Assert.AreEqual(1, order.ValueStack.Count());
            Assert.AreEqual(null, order.ValueStack.First().Value);
            Assert.AreEqual("low", order.ValueStack.First().Substream);
            Assert.AreEqual(FormulaItem.STREAM, order.ValueStack.First().ValueType);
            Assert.AreEqual("low", order.UserInput);
        }

        [TestCategory("Stop/limit parser")]
        [TestMethod]
        public void TestStreamValueMinusValuePips()
        {
            var model = new StrategyModel();
            var parser = new StopLimitParser(model);
            var order = parser.Parse("low - 5");
            Assert.IsNotNull(order);
            Assert.AreEqual(3, order.ValueStack.Count());
            Assert.AreEqual(null, order.ValueStack[0].Value);
            Assert.AreEqual("low", order.ValueStack[0].Substream);
            Assert.AreEqual(FormulaItem.STREAM, order.ValueStack[0].ValueType);
            Assert.AreEqual("-", order.ValueStack[1].Value);
            Assert.AreEqual(FormulaItem.OPERAND, order.ValueStack[1].ValueType);
            Assert.AreEqual("5.0", order.ValueStack[2].Value);
            Assert.AreEqual(FormulaItem.VALUE, order.ValueStack[2].ValueType);
            Assert.AreEqual("low - 5", order.UserInput);
        }

        [TestCategory("Stop/limit parser")]
        [TestMethod]
        public void TestStreamValueMinusParamValuePips()
        {
            var model = new StrategyModel()
            {
                Parameters = new List<StrategyParameter>()
                {
                    new StrategyParameter()
                    {
                        Id = "param_1",
                        ParameterType = StrategyParameter.INTEGER,
                        DefaultValue = "5"
                    }
                }
            };
            var parser = new StopLimitParser(model);
            var order = parser.Parse("low - param_1");
            Assert.IsNotNull(order);
            Assert.AreEqual(3, order.ValueStack.Count());
            Assert.AreEqual(null, order.ValueStack[0].Value);
            Assert.AreEqual("low", order.ValueStack[0].Substream);
            Assert.AreEqual(FormulaItem.STREAM, order.ValueStack[0].ValueType);
            Assert.AreEqual("-", order.ValueStack[1].Value);
            Assert.AreEqual(FormulaItem.OPERAND, order.ValueStack[1].ValueType);
            Assert.AreEqual("param_1", order.ValueStack[2].Value);
            Assert.AreEqual(FormulaItem.PARAM, order.ValueStack[2].ValueType);
            Assert.AreEqual("low - param_1", order.UserInput);
        }

        //TODO: uncomment
        //[TestCategory("Stop/limit parser")]
        //[TestMethod]
        //public void TestFormulaBrackets()
        //{
        //    var model = new StrategyModel();
        //    var parser = new StopLimitParser(model);
        //    var order = parser.Parse("close - (high - close)");
        //    Assert.IsNotNull(order);
        //    Assert.AreEqual("[source.close]-(source.high-source.close)", order.Value);
        //    Assert.AreEqual("close - (high - close)", order.UserInput);
        //}

        [TestCategory("Stop/limit parser")]
        [TestMethod]
        public void TestIndicatorDefaultStreamValue()
        {
            var model = new StrategyModel()
            {
                Sources = new List<Source>()
                {
                    new Source()
                    {
                        SourceType = Source.INDICATOR,
                        Name = "RSI",
                        Id = "rsi"
                    }
                }
            };
            var parser = new StopLimitParser(model);
            var order = parser.Parse("rsi");
            Assert.IsNotNull(order);
            Assert.AreEqual(1, order.ValueStack.Count());
            Assert.AreEqual("rsi", order.ValueStack.First().Value);
            Assert.AreEqual(null, order.ValueStack.First().Substream);
            Assert.AreEqual(FormulaItem.STREAM, order.ValueStack.First().ValueType);
            Assert.AreEqual("rsi", order.UserInput);
        }
    }
}
