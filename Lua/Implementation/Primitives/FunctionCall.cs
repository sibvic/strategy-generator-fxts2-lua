using System.Collections.Generic;
using System.Linq;

namespace ProfitRobots.StrategyGenerator.Lua.Implementation
{
    class FunctionCall : IPrimitive
    {
        string _functionName;
        bool _dedicatedStatement;
        List<IPrimitive> _params = new List<IPrimitive>();
        List<IPrimitive> _validatinChecks = new List<IPrimitive>();

        public FunctionCall(string functionName, bool dedicatedStatement = false)
        {
            _functionName = functionName;
            _dedicatedStatement = dedicatedStatement;
        }

        public FunctionCall AddParameter(string primitive)
        {
            _params.Add(primitive.MakePrimitive());
            return this;
        }

        public FunctionCall AddParameter(IPrimitive primitive)
        {
            _params.Add(primitive);
            return this;
        }

        public FunctionCall AddCondition(IPrimitive primitive)
        {
            _validatinChecks.Add(primitive);
            return this;
        }

        public List<IPrimitive> GetValidationChecks()
        {
            List<IPrimitive> validationChecks = new List<IPrimitive>();
            validationChecks.AddRange(_validatinChecks);
            foreach (var param in _params)
            {
                validationChecks.AddRange(param.GetValidationChecks());
            }
            return validationChecks;
        }

        public List<ConstPrimitive> GetConstants()
        {
            List<ConstPrimitive> constants = new List<ConstPrimitive>();
            foreach (var param in _params)
                constants.AddRange(param.GetConstants());

            return constants;
        }

        public string ToCode()
        {
            return _functionName + "(" + string.Join(", ", _params.Select(p => p.ToCode())) + ")" + (_dedicatedStatement ? ";" : "");
        }
    }
}
