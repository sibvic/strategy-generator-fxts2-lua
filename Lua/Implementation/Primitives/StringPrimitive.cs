using System.Collections.Generic;

namespace ProfitRobots.StrategyGenerator.Lua.Implementation
{
    class StringPrimitive : IPrimitive
    {
        string _value;
        public StringPrimitive(string value)
        {
            _value = value;
        }

        public List<ConstPrimitive> GetConstants()
        {
            return new List<ConstPrimitive>();
        }

        public List<IPrimitive> GetValidationChecks()
        {
            return new List<IPrimitive>();
        }

        public string ToCode()
        {
            return _value;
        }
    }
}
