using System.Collections.Generic;

namespace ProfitRobots.StrategyGenerator.Lua.Implementation
{
    /// <summary>
    /// Stores list of variables
    /// </summary>
    class VariablesStorage
    {
        VariableNameGenerator _namesGenerator = new VariableNameGenerator();
        Dictionary<string, string> _namesCache = new Dictionary<string, string>();

        /// <summary>
        /// Get variable name by it's value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetVariableName(string value)
        {
            if (_namesCache.ContainsKey(value))
            {
                return _namesCache[value];
            }
            return null;
        }

        /// <summary>
        /// Create new variable.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Create(string value)
        {
            var varName = _namesGenerator.Generate();
            _namesCache[value] = varName;
            return varName;
        }
    }
}
