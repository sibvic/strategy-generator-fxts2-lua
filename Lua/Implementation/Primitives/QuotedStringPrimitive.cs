using System.Collections.Generic;

namespace ProfitRobots.StrategyGenerator.Lua.Implementation
{
    class QuotedStringPrimitive : IPrimitive
    {
        string _value;
        public QuotedStringPrimitive(string value)
        {
            _value = value;
        }

        public List<IPrimitive> GetValidationChecks()
        {
            return new List<IPrimitive>();
        }

        public List<ConstPrimitive> GetConstants()
        {
            return new List<ConstPrimitive>();
        }

        public string ToCode()
        {
            return "\"" +_value + "\"";
        }
    }
}
