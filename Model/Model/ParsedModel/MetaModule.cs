using ProfitRobots.StrategyGenerator.Model;
using System.Collections.Generic;

namespace ProfitRobots.StrategyGenerator.Model
{
    /// <summary>
    /// Module.
    /// </summary>
    public class MetaModule
    {
        public MetaModule()
        {
        }

        public string Name { get; set; }

        public List<IParameter> Parameters { get; private set; } = new List<IParameter>();
    }
}
