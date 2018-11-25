using ProfitRobots.StrategyGenerator.Model;
using System.Collections.Generic;
using System.Linq;

namespace ProfitRobots.StrategyGenerator.ModelParser
{
    class ConditionBuilder
    {
        FormulaItemBuilder _firstArgumentBuilder;
        FormulaItemBuilder _secondArgumentBuilder;
        private CompareOperationBuilder _operationBuilder;
        private BuilderSequence _builders;
        StrategyModel _model;

        public ConditionBuilder(StrategyModel model)
        {
            _model = model;
            _firstArgumentBuilder = new FormulaItemBuilder(model);
            _secondArgumentBuilder = new FormulaItemBuilder(model);
            _operationBuilder = new CompareOperationBuilder();
            _builders = new BuilderSequence()
                .Add(_firstArgumentBuilder)
                .Add(_operationBuilder)
                .Add(_secondArgumentBuilder);
        }

        public void AddWord(Token word) => _builders.TryAddWord(word);

        public Condition Build()
        {
            var res = new Condition()
            {
                Conditions = new List<Condition>(),
                Arg1 = _firstArgumentBuilder.Build()
            };
            if (!IsBoolParameter(res.Arg1))
            {
                res.Arg2 = _secondArgumentBuilder.Build();
                res.ConditionType = _operationBuilder.Build();
            }
            else
            {
                res.Arg2 = new FormulaItem()
                {
                    ValueType = FormulaItem.VALUE,
                    Value = "true"
                };
                res.ConditionType = Condition.EQ;
            }
            return res;
        }

        private bool IsBoolParameter(FormulaItem item)
        {
            if (item.ValueType != FormulaItem.PARAM)
                return false;
            var param = _model.Parameters.Where(p => p.Id == item.Value).First();
            return param.ParameterType == StrategyParameter.BOOLEAN;
        }
    }
}
