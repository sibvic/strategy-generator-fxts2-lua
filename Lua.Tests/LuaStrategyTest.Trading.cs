using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProfitRobots.StrategyGenerator.Lua.Tests
{
    public partial class LuaStrategyTest
    {
        [TestMethod]
        [TestCategory("Trading")]
        public void Lua_CalculatedStop()
        {
            DoIncludeTest("trading/calculated_stop");
        }

        [TestMethod]
        [TestCategory("Trading")]
        public void Lua_CalculatedLimit()
        {
            DoIncludeTest("trading/calculated_limit");
        }

        [TestMethod]
        [TestCategory("Trading")]
        public void Lua_PipLimit()
        {
            DoIncludeTest("trading/pip_limit");
        }

        [TestMethod]
        [TestCategory("Trading")]
        public void Lua_PipStop()
        {
            DoIncludeTest("trading/pip_stop");
        }

        [TestMethod]
        [TestCategory("Trading")]
        public void Lua_StopLimitFormula()
        {
            DoIncludeTest("trading/stop_limit_formula");
        }

        [TestMethod]
        [TestCategory("Trading")]
        public void Lua_StopLimitFormulaParam()
        {
            DoIncludeTest("trading/stop_limit_formula_param");
        }
    }
}
