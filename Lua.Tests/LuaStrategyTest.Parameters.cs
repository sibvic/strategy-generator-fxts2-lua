using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProfitRobots.StrategyGenerator.Lua.Tests
{
    public partial class LuaStrategyTest
    {
        [TestMethod]
        [TestCategory("Parameters")]
        public void Lua_IntParameter()
        {
            DoIncludeTest("parameters/int_parameter");
        }

        [TestMethod]
        [TestCategory("Parameters")]
        public void Lua_IntValueFromParameter()
        {
            DoIncludeTest("parameters/int_value_from_parameter.json", "parameters/int_value_from_parameter.lua");
        }

        [TestMethod]
        [TestCategory("Parameters")]
        public void Lua_TimeframeParameter()
        {
            DoIncludeTest("parameters/timeframe_value_from_parameter.json", "parameters/timeframe_value_from_parameter.lua");
        }

        [TestMethod]
        [TestCategory("Parameters")]
        public void Lua_PriceTypeParameter()
        {
            DoIncludeTest("parameters/priceType_value_from_parameter.json", "parameters/priceType_value_from_parameter.lua");
        }

        [TestMethod]
        [TestCategory("Parameters")]
        public void Lua_StringParameter()
        {
            DoIncludeTest("parameters/string_parameter");
        }

        [TestMethod]
        [TestCategory("Parameters")]
        public void Lua_BoolParameter()
        {
            DoIncludeTest("parameters/bool_parameter");
        }

        [TestMethod]
        [TestCategory("Parameters")]
        public void Lua_BoolValueFromParameter()
        {
            DoIncludeTest("parameters/bool_value_from_parameter");
        }

        [TestMethod]
        [TestCategory("Parameters")]
        public void Lua_PriceParameter()
        {
            DoIncludeTest("parameters/price_parameter");
        }

        [TestMethod]
        [TestCategory("Parameters")]
        public void Lua_DoubleParameter()
        {
            DoIncludeTest("parameters/double_parameter.json", "parameters/double_parameter.lua");
        }

        [TestMethod]
        [TestCategory("Parameters")]
        public void Lua_DoubleValueFromParameter()
        {
            DoIncludeTest("parameters/double_value_from_parameter");
        }
    }
}
