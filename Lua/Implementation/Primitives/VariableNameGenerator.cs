using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitRobots.StrategyGenerator.Lua.Implementation
{
    class VariableNameGenerator
    {
        int _next = 1;

        public string Generate()
        {
            return $"_v{_next++}";
        }
    }
}
