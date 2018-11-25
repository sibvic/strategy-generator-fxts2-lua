using System.Collections.Generic;

namespace ProfitRobots.StrategyGenerator.Lua.Implementation
{
    class ConstPrimitive : IPrimitive
    {
        public string ConstName { get; set; }

        IPrimitive _value;
        public ConstPrimitive(IPrimitive value)
        {
            _value = value;
        }

        public string GetConstValue()
        {
            return _value.ToCode();
        }

        public List<IPrimitive> GetValidationChecks()
        {
            return _value.GetValidationChecks();
        }

        public string ToCode()
        {
            if (ConstName != null)
                return ConstName;
            return GetConstValue();
        }

        public List<ConstPrimitive> GetConstants()
        {
            var constants = _value.GetConstants();
            constants.Add(this);
            return constants;
        }
    }
}
