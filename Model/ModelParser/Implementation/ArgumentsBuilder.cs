using ProfitRobots.StrategyGenerator.Model;
using System.Collections.Generic;
using System.Linq;

namespace ProfitRobots.StrategyGenerator.ModelParser.Implementation
{
    public class ArgumentsBuilder
    {
        public static List<FormulaItem> BuildArgs(string text, StrategyModel model)
        {
            var words = Tokens.Parse(text).ToList();
            var argBuilder = new FormulaItemBuilder(model);
            var args = new List<FormulaItem>();
            foreach (var token in words)
            {
                switch (token.TokenType)
                {
                    case Token.Type.Operator:
                        args.Add(argBuilder.Build());
                        args.Add(FormulaItem.BuildeOperand(token.Data));

                        argBuilder = new FormulaItemBuilder(model);
                        break;
                    case Token.Type.Number:
                    case Token.Type.Word:
                        if (!argBuilder.TryAddWord(token))
                        {
                            throw new InvalidSemanticItemException(null, text, message: $"Unknown or unsupported value {token.Data}");
                        }
                        break;
                }
            }
            args.Add(argBuilder.Build());

            return args;
        }
    }
}
