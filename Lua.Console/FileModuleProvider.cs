using ProfitRobots;
using ProfitRobots.StrategyGenerator.Lua;

namespace LuaStrategyGenerator.Console
{
    class FileModuleProvider : IModuleProvider
    {
        public string[] GetCode(string moduleName)
        {
            var location = System.Reflection.Assembly.GetEntryAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(location);
            string fileName = System.IO.Path.Combine(directory, "snippets", moduleName + ".lua");
            return System.IO.File.ReadAllLines(fileName);
        }
    }
}
