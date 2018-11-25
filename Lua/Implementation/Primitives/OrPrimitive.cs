using System.Collections.Generic;
using System.Linq;

namespace ProfitRobots.StrategyGenerator.Lua.Implementation
{
    class OrPrimitive : IPrimitive
    {
        List<IPrimitive> _params = new List<IPrimitive>();

        public OrPrimitive()
        {
        }

        public OrPrimitive AddParameter(string primitive)
        {
            if (primitive == null)
                return this;
            _params.Add(primitive.MakePrimitive());
            return this;
        }

        public OrPrimitive AddParameter(IPrimitive primitive)
        {
            if (primitive == null)
                return this;
            _params.Add(primitive);
            return this;
        }

        public List<IPrimitive> GetValidationChecks()
        {
            var validationChecks = new List<IPrimitive>();
            foreach (var param in _params)
                validationChecks.AddRange(param.GetValidationChecks());

            return validationChecks;
        }

        public List<ConstPrimitive> GetConstants()
        {
            var constants = new List<ConstPrimitive>();
            foreach (var param in _params)
                constants.AddRange(param.GetConstants());

            return constants;
        }

        public string ToCode()
        {
            return "(" + string.Join(" or ", _params.Select(p => p.ToCode())) + ")";
        }
    }
}
