using ProfitRobots.StrategyGenerator.Model;
using ProfitRobots.StrategyGenerator.ModelParser.Implementation;
using System.Linq;

namespace ProfitRobots.StrategyGenerator.ModelParser
{
    public class SourceParser
    {
        StrategyModel _model;
        public SourceParser(StrategyModel model)
        {
            _model = model;
        }

        /// <summary>
        /// Parse source id
        /// </summary>
        /// <param name="text">Text to parse</param>
        /// <exception cref="InvalidSemanticItemException"></exception>
        /// <exception cref="NotEnoughDataException"></exception>
        /// <returns>Parsed source</returns>
        public FormulaItem Parse(string text)
        {
            var words = text.Split(new char[] { ' ', '\n' }).Select(w => new Token() { Data = w }).ToList();
            var builder = new FormulaItemBuilder(_model);
            foreach (var token in words)
            {
                if (!builder.TryAddWord(token))
                {
                    throw new InvalidSemanticItemException(_model, token.Data);
                }
            }

            var res = builder.Build();
            res.UsersInput = text;
            return res;
        }
    }
}
