using System.Collections.Generic;
using System.Linq;

namespace ProfitRobots.StrategyGenerator.Lua.Implementation
{
    class AndPrimitive : IPrimitive
    {
        List<IPrimitive> _params = new List<IPrimitive>();

        public AndPrimitive()
        {
        }

        public AndPrimitive AddParameter(string primitive)
        {
            if (primitive == null)
                return this;
            _params.Add(primitive.MakePrimitive());
            return this;
        }

        public AndPrimitive AddParameter(IPrimitive primitive)
        {
            if (primitive == null)
                return this;
            _params.Add(primitive);
            return this;
        }

        public List<ConstPrimitive> GetConstants()
        {
            var constants = new List<ConstPrimitive>();
            foreach (var param in _params)
                constants.AddRange(param.GetConstants());

            return constants;
        }

        public List<IPrimitive> GetValidationChecks()
        {
            var validationChecks = new List<IPrimitive>();
            foreach (var param in _params)
                validationChecks.AddRange(param.GetValidationChecks());

            return validationChecks;
        }

        public string ToCode()
        {
            return "(" + string.Join(" and ", _params.Select(p => p.ToCode())) + ")";
        }
    }
}
