using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProfitRobots.StrategyGenerator.Model;
using Newtonsoft.Json;
using System;
using System.Text;
using ProfitRobots.StrategyGenerator.ModelParser;

namespace ProfitRobots.StrategyGenerator.Lua.Tests
{
    [TestClass]
    public partial class LuaStrategyTest
    {
        private static string GetTargetResult(string luaName)
        {
            return System.IO.File.ReadAllText(System.IO.Path.Combine("../../../proper_results", luaName));
        }

        private static string GetJson(string jsonName)
        {
            return System.IO.File.ReadAllText(System.IO.Path.Combine("../../../../../test_json", jsonName));
        }

        private static string GetDiff(string targetResult, string result)
        {
            StringBuilder diff = new StringBuilder();
            string[] targetResultStrings = targetResult.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            string[] resultStrings = result.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < Math.Min(targetResultStrings.Length, resultStrings.Length); ++i)
            {
                if (targetResultStrings[i] != resultStrings[i])
                {
                    diff.AppendFormat("\nExpected:{0}\nResult  :{1}", targetResultStrings[i], resultStrings[i]);
                }
            }
            return diff.ToString();
        }

        private static void DoTest(string jsonFile, string luaFile)
        {
            string json = GetJson(jsonFile);
            string targetResult = GetTargetResult(luaFile);
            var model = JsonConvert.DeserializeObject<StrategyModel>(json);
            var generator = new StrategyGenerator(new FileModuleProvider());
            var metaModel = MetaModelFactory.Create(model);
            var result = generator.Generate(metaModel);
            Assert.AreEqual(targetResult, result, string.Format("Sources not equal: {0}", GetDiff(targetResult, result)));
        }

        private static void DoIncludeTest(string name)
        {
            DoIncludeTest(name + ".json", name + ".lua");
        }

        private static void DoIncludeTest(string jsonFile, string luaFile)
        {
            string json = GetJson(jsonFile);
            string targetResult = GetTargetResult(luaFile);
            var model = JsonConvert.DeserializeObject<StrategyModel>(json);
            var generator = new StrategyGenerator(new FileModuleProvider());
            var metaModel = MetaModelFactory.Create(model);
            var result = generator.Generate(metaModel);
            Assert.IsTrue(result.Contains(targetResult), string.Format("Target code not found: \n{0}\n in \n{1}", targetResult, result));
        }

        [TestMethod]
        public void Lua_Debug()
        {
            DoIncludeTest("debug");
        }

        [TestMethod]
        public void Lua_DebugIndicator()
        {
            DoIncludeTest("debug_indicator");
        }

        [TestMethod]
        public void Lua_Name()
        {
            DoIncludeTest("name");
        }

        [TestMethod]
        public void Lua_CustomId()
        {
            DoIncludeTest("custom_id");
        }
    }
}
