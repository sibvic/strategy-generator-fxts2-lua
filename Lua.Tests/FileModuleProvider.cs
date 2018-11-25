namespace ProfitRobots.StrategyGenerator.Lua.Tests
{
    class FileModuleProvider : IModuleProvider
    {
        public string[] GetCode(string moduleName)
        {
            string fileName = System.IO.Path.Combine("../../../../Lua/snippets", moduleName + ".lua");
            return System.IO.File.ReadAllLines(fileName);
        }
    }
}
