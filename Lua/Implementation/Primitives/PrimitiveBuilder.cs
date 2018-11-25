namespace ProfitRobots.StrategyGenerator.Lua.Implementation.Primitives
{
    class PrimitiveBuilder
    {
        public class IfPrimitiveBuilder
        {
            private IPrimitive _condition;
            private IPrimitive _trueStatement;

            public IfPrimitiveBuilder(IPrimitive condition)
            {
                _condition = condition;
            }

            public IfPrimitiveBuilder Then(IPrimitive trueStatement)
            {
                _trueStatement = trueStatement;
                return this;
            }

            public IfStatement Build(VariablesStorage variables)
            {
                return new IfStatement(_condition, _trueStatement, variables);
            }
        }

        public static IfPrimitiveBuilder If(string condition)
        {
            return new IfPrimitiveBuilder(condition.MakePrimitive());
        }
    }
}
