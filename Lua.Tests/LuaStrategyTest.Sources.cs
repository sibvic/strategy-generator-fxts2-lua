using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProfitRobots.StrategyGenerator.Lua.Tests
{
    public partial class LuaStrategyTest
    {
        [TestMethod]
        public void Lua_IndicatorOnInstrument()
        {
            DoIncludeTest("sources/indicator_on_instrument.json", "sources/indicator_on_instrument.lua");
        }

        [TestMethod]
        public void Lua_IndicatorOnMainSource()
        {
            DoIncludeTest("sources/indicator_on_main_source.json", "sources/indicator_on_main_source.lua");
        }

        [TestMethod]
        public void Lua_MvaOnMainSourceClose()
        {
            DoIncludeTest("sources/mva_on_main_source_close.json", "sources/mva_on_main_source_close.lua");
        }

        [TestMethod]
        public void Lua_Rsi()
        {
            DoIncludeTest("sources/rsi");
        }

        [TestMethod]
        public void Lua_Macd()
        {
            DoIncludeTest("sources/macd");
        }

        [TestMethod]
        public void Lua_Stochastic()
        {
            DoIncludeTest("sources/Stochastic");
        }

        [TestMethod]
        public void Lua_MvaWithParameters()
        {
            DoIncludeTest("sources/mva_with_parameters");
        }

        [TestMethod]
        public void Lua_RsiWithParameters()
        {
            DoIncludeTest("sources/rsi_with_parameters");
        }

        [TestMethod]
        public void Lua_MacdWithParameters()
        {
            DoIncludeTest("sources/macd_with_parameters");
        }

        [TestMethod]
        public void Lua_StochWithParameters()
        {
            DoIncludeTest("sources/stochastic_with_parameters");
        }

        [TestMethod]
        public void Lua_IndicatorWithExternalParameters()
        {
            DoIncludeTest("sources/indicator_with_external_parameters");
        }

        [TestMethod]
        public void Lua_IndicatorBB()
        {
            DoIncludeTest("sources/bb");
        }

        [TestMethod]
        public void Lua_IndicatorBBWithParameters()
        {
            DoIncludeTest("sources/bb_with_parameters");
        }

        [TestMethod]
        public void Lua_IndicatorSAR()
        {
            DoIncludeTest("sources/sar");
        }

        [TestMethod]
        public void Lua_IndicatorSARWithParameters()
        {
            DoIncludeTest("sources/sar_with_parameters");
        }

        [TestMethod]
        public void Lua_IndicatorATR()
        {
            DoIncludeTest("sources/atr");
        }

        [TestMethod]
        public void Lua_IndicatorATRWithParameters()
        {
            DoIncludeTest("sources/atr_with_parameters");
        }

        [TestMethod]
        public void Lua_IndicatorCCI()
        {
            DoIncludeTest("sources/cci");
        }

        [TestMethod]
        public void Lua_IndicatorCCIWithParameters()
        {
            DoIncludeTest("sources/cci_with_parameters");
        }

        [TestMethod]
        public void Lua_IndicatorEMA()
        {
            DoIncludeTest("sources/ema");
        }

        [TestMethod]
        public void Lua_IndicatorEMAWithParameters()
        {
            DoIncludeTest("sources/ema_with_parameters");
        }

        [TestMethod]
        public void Lua_IndicatorTSI()
        {
            DoIncludeTest("sources/tsi");
        }

        [TestMethod]
        public void Lua_IndicatorTSIWithParameters()
        {
            DoIncludeTest("sources/tsi_with_parameters");
        }

        [TestMethod]
        public void Lua_IndicatorVWAP()
        {
            DoIncludeTest("sources/vwap");
        }

        [TestMethod]
        public void Lua_IndicatorWMA()
        {
            DoIncludeTest("sources/wma");
        }

        [TestMethod]
        public void Lua_IndicatorWMAWithParameters()
        {
            DoIncludeTest("sources/wma_with_parameters");
        }
    }
}
