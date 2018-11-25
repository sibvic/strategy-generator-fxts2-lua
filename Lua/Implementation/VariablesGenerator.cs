using ProfitRobots.StrategyGenerator.Model;
using System.Collections.Generic;

namespace ProfitRobots.StrategyGenerator.Lua.Implementation
{
    /// <summary>
    /// Generates code of the variables.
    /// </summary>
    class VariablesGenerator
    {
        MetaModel _model;
        int _lastCoookie = 0;
        private VariablesGenerator(MetaModel strategyModel)
        {
            _model = strategyModel;
        }

        /// <summary>
        /// Adds code to the string builder.
        /// </summary>
        /// <param name="strategyModel">Model</param>
        public static IPrimitive GenerateCode(MetaModel strategyModel)
        {
            return new VariablesGenerator(strategyModel).Generate();
        }

        public IPrimitive Generate()
        {
            var code = new CodeBlock();
            code.Add("local last_serial;")
                .Add("local custom_id;");
            if (_model.Sources != null)
            {
                foreach (var source in _model.Sources)
                {
                    code.Add(GenerateVariableForSource(source));
                }
            }
            return code;
        }

        private List<IPrimitive> GenerateVariableForSource(IMetaSource source)
        {
            var code = new List<IPrimitive>();
            var sourceId = source.Id;
            switch (source)
            {
                case IndicatorSource indicatorSource:
                    code.Add($"local {sourceId} = nil;".MakePrimitive());
                    break;
                case InstrumentSource instrumentSource:
                    {
                        code.Add($"local {sourceId} = nil;".MakePrimitive());
                        var sourceIdCookie = ++_lastCoookie;
                        code.Add($"local {sourceId}_id = {sourceIdCookie};".MakePrimitive());
                    }
                    break;
            }
            return code;
        }
    }
}
