using System;
using System.Collections.Generic;
using System.Linq;

namespace ProfitRobots.StrategyGenerator.Lua.Implementation
{
    class IfStatement : IPrimitive
    {
        IPrimitive _condition;
        IPrimitive _trueStatement;
        VariablesStorage _variables;
        private readonly string _prefix;

        public IfStatement(IPrimitive condition, IPrimitive trueStatement, VariablesStorage variables, string prefix = "")
        {
            _condition = condition;
            _trueStatement = trueStatement;
            _variables = variables;
            _prefix = prefix;
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
            var lines = new List<string>();

            foreach (var constant in _condition.GetConstants().Where(c => c.ConstName == null))
            {
                string varValue = constant.GetConstValue();
                string varName = _variables.GetVariableName(varValue);
                if (varName == null)
                {
                    varName = _variables.Create(varValue);
                    lines.Add(_prefix + $"local {varName} = {constant.GetConstValue()};");
                }
                constant.ConstName = varName;
            }
            var validationCheks = FormatValidationChecks();
            lines.Add(_prefix + $"if ({validationCheks}{_condition.ToCode()}) then");
            foreach (var line in _trueStatement.ToCode().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                lines.Add(_prefix + $"    {line}");
            }
            lines.Add(_prefix + "end\r\n");
            return string.Join("\r\n", lines);
        }

        private string FormatValidationChecks()
        {
            List<IPrimitive> validations = _condition.GetValidationChecks();
            if (validations.Count > 0)
                return "(" + string.Join(" and ", validations.Select(vc => vc.ToCode()).Distinct()) + ") and ";
            return "";
        }

    }
}
