using System.Collections.Generic;

namespace ProfitRobots.StrategyGenerator.Lua.Implementation
{
    class AssigmentStatement : IPrimitive
    {
        string _variableName;
        IPrimitive _value;
        public AssigmentStatement(string variableName, IPrimitive value)
        {
            _variableName = variableName;
            _value = value;
        }

        public List<ConstPrimitive> GetConstants()
        {
            return _value.GetConstants();
        }

        public List<IPrimitive> GetValidationChecks()
        {
            return _value.GetValidationChecks();
        }

        public string ToCode()
        {
            return $"{_variableName} = {_value.ToCode()};";
        }
    }
}
