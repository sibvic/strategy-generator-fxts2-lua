using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitRobots.StrategyGenerator.Lua.Implementation
{
    class NotEqualPrimitive : IPrimitive
    {
        private readonly IPrimitive _arg1;
        private readonly IPrimitive _arg2;

        public NotEqualPrimitive(string arg1, string arg2)
            :this(arg1.MakePrimitive(), arg2.MakePrimitive())
        {

        }

        public NotEqualPrimitive(IPrimitive arg1, IPrimitive arg2)
        {
            _arg1 = arg1;
            _arg2 = arg2;
        }

        public List<ConstPrimitive> GetConstants()
        {
            var constPrimitives = new List<ConstPrimitive>();
            constPrimitives.AddRange(_arg1.GetConstants());
            constPrimitives.AddRange(_arg2.GetConstants());
            return constPrimitives;
        }

        public List<IPrimitive> GetValidationChecks()
        {
            var validationChecks = new List<IPrimitive>();
            validationChecks.AddRange(_arg1.GetValidationChecks());
            validationChecks.AddRange(_arg2.GetValidationChecks());
            return validationChecks;
        }

        public string ToCode()
        {
            return $"{_arg1.ToCode()} ~= {_arg2.ToCode()}";
        }
    }
}
