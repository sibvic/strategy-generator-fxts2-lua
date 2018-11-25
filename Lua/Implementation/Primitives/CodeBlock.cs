using System.Collections.Generic;
using System.Text;

namespace ProfitRobots.StrategyGenerator.Lua.Implementation
{
    /// <summary>
    /// Block of code.
    /// </summary>
    class CodeBlock : IPrimitive
    {
        List<IPrimitive> _items = new List<IPrimitive>();
        public CodeBlock Add(IPrimitive primitive)
        {
            _items.Add(primitive);
            return this;
        }

        public CodeBlock Add(IEnumerable<IPrimitive> primitives)
        {
            _items.AddRange(primitives);
            return this;
        }

        public CodeBlock Add(string line)
        {
            _items.Add(line.MakePrimitive());
            return this;
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
            var code = new StringBuilder();
            foreach (var item in _items)
            {
                string lineCode = item.ToCode();
                if (lineCode == "")
                    continue;
                if (lineCode.EndsWith("\n"))
                    code.Append(lineCode);
                else
                    code.AppendLine(lineCode);
            }
            return code.ToString();
        }
    }
}
