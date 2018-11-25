using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProfitRobots.StrategyGenerator.Lua.Tests
{
    public partial class LuaStrategyTest
    {
        [TestMethod]
        [TestCategory("Modules")]
        public void Lua_ModuleParameters()
        {
            DoIncludeTest("modules/module_parameters");
        }

        [TestMethod]
        [TestCategory("Modules")]
        public void Lua_TradingModule()
        {
            DoIncludeTest("modules/trading");
        }
    }
}
