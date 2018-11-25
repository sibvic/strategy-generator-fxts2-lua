using System.Collections.Generic;

namespace ProfitRobots.StrategyGenerator.Lua.Implementation
{
    /// <summary>
    /// Formats call with the paremeters.
    /// </summary>
    class MethodCallFormatter
    {
        private string _name;
        public List<string> Parameters { get; } = new List<string>();
        public MethodCallFormatter(string name)
        {
            _name = name;
        }

        public string Format()
        {
            string parameters = string.Join(", ", Parameters);
            return $"{_name}({parameters})";
        }
    }
}
