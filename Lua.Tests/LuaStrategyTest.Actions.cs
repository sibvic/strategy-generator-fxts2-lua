using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProfitRobots.StrategyGenerator.Lua.Tests
{
    public partial class LuaStrategyTest
    {
        [TestMethod]
        [TestCategory("Actions")]
        public void Lua_ActionBuy()
        {
            DoIncludeTest("action/buy.json", "action/buy.lua");
        }

        [TestMethod]
        [TestCategory("Actions")]
        public void Lua_ActionSell()
        {
            DoIncludeTest("action/sell.json", "action/sell.lua");
        }

        [TestMethod]
        [TestCategory("Actions")]
        public void Lua_ActionEntryBuy()
        {
            DoIncludeTest("action/entry_buy.json", "action/entry_buy.lua");
        }

        [TestMethod]
        [TestCategory("Actions")]
        public void Lua_ActionEntrySell()
        {
            DoIncludeTest("action/entry_sell.json", "action/entry_sell.lua");
        }

        [TestMethod]
        [TestCategory("Actions")]
        public void Lua_ActionCustomizable()
        {
            DoIncludeTest("action/customizable.json", "action/customizable.lua");
        }

        [TestMethod]
        [TestCategory("Actions")]
        public void Lua_ActionExit()
        {
            DoIncludeTest("action/exit.json", "action/exit.lua");
        }

        [TestMethod]
        [TestCategory("Actions")]
        public void Lua_ActionExitBuy()
        {
            DoIncludeTest("action/exit_buy.json", "action/exit_buy.lua");
        }

        [TestMethod]
        [TestCategory("Actions")]
        public void Lua_ActionExitSell()
        {
            DoIncludeTest("action/exit_sell.json", "action/exit_sell.lua");
        }
    }
}
