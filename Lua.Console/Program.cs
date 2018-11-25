using Newtonsoft.Json;
using ProfitRobots.StrategyGenerator.Model;

namespace LuaStrategyGenerator.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                System.Console.WriteLine("Not enough parameters. Please specify the input json file.");
                return;
            }
            var jsonFile = args[0];
            var luaFile = System.IO.Path.ChangeExtension(jsonFile, "lua");
            var json = System.IO.File.ReadAllText(jsonFile);

            var model = JsonConvert.DeserializeObject<StrategyModel>(json);
            var generator = new ProfitRobots.StrategyGenerator.Lua.StrategyGenerator(new FileModuleProvider());
            try
            {
                var metaModel = ProfitRobots.StrategyGenerator.ModelParser.MetaModelFactory.Create(model);
                var luaCode = generator.Generate(metaModel);

                System.IO.File.WriteAllText(luaFile, luaCode);
            }
            catch (StrategyGeneratorException e)
            {
                System.Console.WriteLine(e);
            }
        }
    }
}