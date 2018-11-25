using ProfitRobots.StrategyGenerator.Model;
using System.Collections.Generic;
using System.Linq;

namespace ProfitRobots.StrategyGenerator.ModelParser
{
    /// <summary>
    /// Converts human-readable test into the action model.
    /// </summary>
    public class ConditionParser
    {
        List<IItemParser> _parsers = new List<IItemParser>();
        StrategyModel _model;
        public ConditionParser(StrategyModel model)
        {
            _model = model;
        }

        /// <summary>
        /// Parses condition from human-readable text.
        /// </summary>
        /// <param name="text">Text to parse</param>
        /// <exception cref="InvalidSemanticItemException"></exception>
        /// <exception cref="NotEnoughDataException"></exception>
        /// <returns>Parsed condition</returns>
        public Condition Parse(string text)
        {
            var words = text.Split(new char[] { ' ', '\n' }).Select(w => new Token() { Data = w }).ToList();
            //ParseWords(words);
            //ApplyResults(words);

            var andConditions = new List<Condition>();
            var builder = new ConditionBuilder(_model);
            foreach (var token in words)
            {
                switch (token.Data)
                {
                    case "and":
                        andConditions.Add(builder.Build());
                        builder = new ConditionBuilder(_model);
                        break;
                    default:
                        builder.AddWord(token);
                        break;
                }
            }

            var condition = builder.Build();
            if (andConditions.Count == 0)
            {
                condition.UserInput = text;
                return condition;
            }
            andConditions.Add(condition);
            return new Condition()
            {
                ConditionType = Condition.AND,
                UserInput = text,
                Conditions = andConditions
            };
        }

        private void ParseWords(List<Token> words)
        {
            foreach (var word in words)
            {
                foreach (var parser in _parsers)
                {
                    parser.ParseWord(word);
                }
            }
        }

        private void ApplyResults(List<Token> words)
        {
            var wordsToProccess = words.Select(w => w);
            while (wordsToProccess.Any())
            {
                var word = wordsToProccess.First();
                var result = ApplyResult(word.Results);
                if (result != null)
                {
                    wordsToProccess = wordsToProccess.Where(w => !result.Tokens.Contains(w));
                }
                else
                {
                    wordsToProccess = wordsToProccess.Skip(1);
                }
            }
        }

        private IParsingResult ApplyResult(IEnumerable<IParsingResult> results)
        {
            foreach (var result in results.OrderByDescending(r => r.Priority))
            {
                //switch (result)
                //{
                    //case DateParsingResult dateResult:
                    //    _item.Date = dateResult.Date;
                    //    return dateResult;
                    //case ItemTypeParsingResult itemTypeResult:
                    //    _item.ItemType = itemTypeResult.ItemType;
                    //    return itemTypeResult;
                    //case MilageParsingResult milageResult:
                    //    _item.Milage = milageResult.Milage;
                    //    return milageResult;
                    //case TotalPriceParsingResult totalPriceResult:
                    //    _item.TotalPrice = totalPriceResult.TotalPrice;
                    //    return totalPriceResult;
                    //case QuantityParsingResult quantityResult:
                    //    _item.Quantity = quantityResult.Quantity;
                    //    return quantityResult;
                    //case PriceParsingResult priceResult:
                    //    _item.Price = priceResult.Price;
                    //    return priceResult;
                //}
            }
            return null;
        }
    }
}
