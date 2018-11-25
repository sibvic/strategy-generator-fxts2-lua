using System.Collections.Generic;

namespace ProfitRobots.StrategyGenerator.Lua.Implementation
{
    class ComparePrimitive : IPrimitive
    {
        IPrimitive _v1;
        IPrimitive _v2;
        string _compare;

        public ComparePrimitive(IPrimitive v1, string compare, IPrimitive v2)
        {
            _v1 = v1;
            _v2 = v2;
            _compare = compare;
        }

        public List<ConstPrimitive> GetConstants()
        {
            List<ConstPrimitive> constants = new List<ConstPrimitive>();
            constants.AddRange(_v1.GetConstants());
            constants.AddRange(_v2.GetConstants());

            return constants;
        }

        public List<IPrimitive> GetValidationChecks()
        {
            var validationChecks = new List<IPrimitive>();
            validationChecks.AddRange(_v1.GetValidationChecks());
            validationChecks.AddRange(_v2.GetValidationChecks());
            return validationChecks;
        }

        public string ToCode()
        {
            return _v1.ToCode() + " " + _compare + " " + _v2.ToCode();
        }
    }
}
