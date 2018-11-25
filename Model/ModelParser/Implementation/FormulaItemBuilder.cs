using ProfitRobots.StrategyGenerator.Model;
using ProfitRobots.StrategyGenerator.ModelParser.ItemParsers;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ProfitRobots.StrategyGenerator.ModelParser
{
    /// <summary>
    /// Builds condition argument.
    /// </summary>
    class FormulaItemBuilder : IBuilder
    {
        StrategyModel model;
        List<IItemParser> _parsers = new List<IItemParser>();
        public FormulaItemBuilder(StrategyModel model)
        {
            _parsers.Add(new DefaultStreamItemParser());
            _parsers.Add(new NumberItemParser());
            if (model.Parameters != null)
            {
                var parameters = model.Parameters;
                if (parameters.Count() > 0)
                    _parsers.Add(new ParameterItemParser(parameters));
            }
                
            this.model = model;
        }

        List<Token> _tokens = new List<Token>();
        FormulaItem formulaItem;

        public bool TryAddWord(Token word)
        {
            bool parsed = false;
            foreach (var parser in _parsers)
            {
                if (parser.ParseWord(word))
                {
                    parsed = true;
                }
            }
            if (parsed)
            {
                _tokens.Add(word);
            }
            else
            {
                if (TryApplyStream(word.Data))
                {
                    parsed = true;
                }
            }
            return parsed;
        }

        private bool TryApplyStream(string word)
        {
            if (formulaItem != null)
                return false;
            var stream = model?.Sources?.Where(s => s.Id == word).FirstOrDefault();
            if (stream != null)
            {
                formulaItem = new FormulaItem()
                {
                    Value = word,
                    Substream = stream.SourceType == Source.INDICATOR ? null : "",
                    ValueType = FormulaItem.STREAM,
                    StreamType = stream.SourceType == Source.INDICATOR ? FormulaItem.INDICATOR : FormulaItem.INSTRUMENT
                };
                return true;
            }
            return TryApplyStreamWithSubstream(word);
        }

        private bool TryApplyStreamWithSubstream(string word)
        {
            if (formulaItem != null)
                return false;

            if (word.Contains("."))
            {
                var streamAndSubstream = word.Split('.');
                if (streamAndSubstream.Count() == 2)
                {
                    var source = model?.Sources?.Where(s => s.Id == streamAndSubstream[0])?.FirstOrDefault();
                    if (model != null && source == null)
                    {
                        return false;
                    }
                    formulaItem = new FormulaItem()
                    {
                        Value = streamAndSubstream[0],
                        Substream = streamAndSubstream[1],
                        ValueType = FormulaItem.STREAM,
                        StreamType = source.SourceType == Source.INDICATOR ? FormulaItem.INDICATOR : FormulaItem.INSTRUMENT
                    };
                    return true;
                }
            }
            if (model == null)
            {
                var source = model.Sources?.Where(s => s.Id == word)?.FirstOrDefault();
                if (source == null)
                    return false;
                formulaItem = new FormulaItem()
                {
                    Value = word,
                    ValueType = FormulaItem.STREAM,
                    StreamType = source.SourceType == Source.INDICATOR ? FormulaItem.INDICATOR : FormulaItem.INSTRUMENT
                };
                return true;
            }
            return false;
        }

        /// <summary>
        /// Builds an condition argument
        /// </summary>
        /// <exception cref="NotEnoughDataException">When there is not enought data to build the argument.</exception>
        /// <returns>Condition argument</returns>
        public FormulaItem Build()
        {
            ApplyResults(_tokens);
            if (formulaItem != null)
                return formulaItem;
            throw new NotEnoughDataException(model, $"Stream and/or substream is not specified");
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
                switch (result)
                {
                    case DefaultStreamParsingResult defaultStreamResult:
                        formulaItem = new FormulaItem()
                        {
                            Substream = defaultStreamResult.StreamName,
                            ValueType = FormulaItem.STREAM,
                            StreamType = FormulaItem.INSTRUMENT
                        };
                        return defaultStreamResult;
                    case NumberParsingResult numberResult:
                        formulaItem = new FormulaItem()
                        {
                            Value = numberResult.Number.ToString("0.0#", CultureInfo.InvariantCulture),
                            ValueType = FormulaItem.VALUE,
                            StreamType = FormulaItem.NOT_USED
                        };
                        return numberResult;
                    case ParameterParsingResult parameterResult:
                        formulaItem = new FormulaItem()
                        {
                            Value = parameterResult.ParameterName,
                            ValueType = FormulaItem.PARAM,
                            StreamType = FormulaItem.NOT_USED
                        };
                        return parameterResult;
                }
            }
            return null;
        }

    }
}
