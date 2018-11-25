using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProfitRobots.StrategyGenerator.Lua.Tests
{
    public partial class LuaStrategyTest
    {
        [TestMethod]
        [TestCategory("Conditions")]
        public void Lua_IndicatorCrossesValue()
        {
            DoIncludeTest("condition/condition_crosses_value.json", "condition/condition_crosses_value.lua");
        }

        [TestMethod]
        [TestCategory("Conditions")]
        public void Lua_IndicatorCrossesOverValue()
        {
            DoIncludeTest("condition/condition_crossesOver_value.json", "condition/condition_crossesOver_value.lua");
        }

        [TestMethod]
        [TestCategory("Conditions")]
        public void Lua_IndicatorCrossesOverOrTouchValue()
        {
            DoIncludeTest("condition/condition_crossesOverOrTouch_value.json", "condition/condition_crossesOverOrTouch_value.lua");
        }

        [TestMethod]
        [TestCategory("Conditions")]
        public void Lua_IndicatorCrossesUnderValue()
        {
            DoIncludeTest("condition/condition_crossesUnder_value.json", "condition/condition_crossesUnder_value.lua");
        }

        [TestMethod]
        [TestCategory("Conditions")]
        public void Lua_IndicatorCrossesUnderOrTouchValue()
        {
            DoIncludeTest("condition/condition_crossesUnderOrTouch_value.json", "condition/condition_crossesUnderOrTouch_value.lua");
        }

        [TestMethod]
        [TestCategory("Conditions")]
        public void Lua_IndicatorCrossesInstrumentClose()
        {
            DoIncludeTest("condition/crosses_instrument_close.json", "condition/crosses_instrument_close.lua");
        }

        [TestMethod]
        [TestCategory("Conditions")]
        public void Lua_IndicatorCrossFixedPeriodsBack()
        {
            DoIncludeTest("condition/cross_fixed_periods_back.json", "condition/cross_fixed_periods_back.lua");
        }

        [TestMethod]
        [TestCategory("Conditions")]
        public void Lua_IndicatorCrossDifferentTFShift()
        {
            DoIncludeTest("condition/cross_different_tf_shift.json", "condition/cross_different_tf_shift.lua");
        }

        [TestMethod]
        [TestCategory("Conditions")]
        public void Lua_GTPrice()
        {
            DoIncludeTest("condition/gt_price");
        }

        [TestMethod]
        [TestCategory("Conditions")]
        public void Lua_GTStreamValue()
        {
            DoIncludeTest("condition/gt_stream_value");
        }

        [TestMethod]
        [TestCategory("Conditions")]
        public void Lua_GTStream()
        {
            DoIncludeTest("condition/gt_stream.json", "condition/gt_stream.lua");
        }

        [TestMethod]
        [TestCategory("Conditions")]
        public void Lua_LTStreamValue()
        {
            DoIncludeTest("condition/lt_stream_value.json", "condition/lt_stream_value.lua");
        }

        [TestMethod]
        [TestCategory("Conditions")]
        public void Lua_LTEStreamValue()
        {
            DoIncludeTest("condition/lte_stream_value.json", "condition/lte_stream_value.lua");
        }

        [TestMethod]
        [TestCategory("Conditions")]
        public void Lua_GTEStreamValue()
        {
            DoIncludeTest("condition/gte_stream_value.json", "condition/gte_stream_value.lua");
        }

        [TestMethod]
        [TestCategory("Conditions")]
        public void Lua_EQStreamValue()
        {
            DoIncludeTest("condition/eq_stream_value.json", "condition/eq_stream_value.lua");
        }

        [TestMethod]
        [TestCategory("Conditions")]
        public void Lua_NEQStreamValue()
        {
            DoIncludeTest("condition/neq_stream_value.json", "condition/neq_stream_value.lua");
        }

        [TestMethod]
        [TestCategory("Conditions")]
        public void Lua_And()
        {
            DoIncludeTest("condition/and.json", "condition/and.lua");
        }

        [TestMethod]
        [TestCategory("Conditions")]
        public void Lua_Or()
        {
            DoIncludeTest("condition/or.json", "condition/or.lua");
        }

        [TestMethod]
        [TestCategory("Conditions")]
        public void Lua_CrossStream()
        {
            DoIncludeTest("condition/cross_stream.json", "condition/cross_stream.lua");
        }

        [TestMethod]
        [TestCategory("Conditions")]
        public void Lua_CrossParamValue()
        {
            DoIncludeTest("condition/condition_crosses_param_value.json", "condition/condition_crosses_param_value.lua");
        }
    }
}
