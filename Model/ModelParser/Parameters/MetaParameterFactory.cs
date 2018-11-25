using ProfitRobots.StrategyGenerator.Model;
using ProfitRobots.StrategyGenerator.Model;
using System;
using System.Globalization;

namespace ProfitRobots.StrategyGenerator.ModelParser
{
    class MetaParameterFactory
    {
        /// <summary>
        /// Create parameter
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">In case of bad default value.</exception>
        /// <param name="parameter">Parameter to parse</param>
        /// <returns>Parsed parameter</returns>
        public static IParameter Create(StrategyParameter parameter)
        {
            switch (parameter.ParameterType)
            {
                case StrategyParameter.INTEGER:
                    return new IntParameter()
                    {
                        Id = parameter.Id,
                        Name = parameter.Name,
                        Description = parameter.Description,
                        Value = int.Parse(parameter.DefaultValue)
                    };
                case StrategyParameter.DOUBLE:
                    return new DoubleParameter()
                    {
                        Id = parameter.Id,
                        Name = parameter.Name,
                        Description = parameter.Description,
                        Value = double.Parse(parameter.DefaultValue.Replace(",", "."), NumberStyles.Float, CultureInfo.InvariantCulture)
                    };
                case StrategyParameter.PRICE:
                    return new PriceParameter()
                    {
                        Id = parameter.Id,
                        Name = parameter.Name,
                        Description = parameter.Description,
                        Value = double.Parse(parameter.DefaultValue.Replace(",", "."), NumberStyles.Float, CultureInfo.InvariantCulture)
                    };
                case StrategyParameter.PRICE_TYPE:
                    try
                    {
                        return new PriceTypeParameter()
                        {
                            Id = parameter.Id,
                            Name = parameter.Name,
                            Description = parameter.Description,
                            Value = ParsePriceType(parameter.DefaultValue)
                        };
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        throw new ArgumentOutOfRangeException(parameter.Id);
                    }
                case StrategyParameter.TIMEFRAME:
                    return new TimeframeParameter()
                    {
                        Id = parameter.Id,
                        Name = parameter.Name,
                        Description = parameter.Description,
                        Value = parameter.DefaultValue
                    };
                case StrategyParameter.BOOLEAN:
                    return new BoolParameter()
                    {
                        Id = parameter.Id,
                        Name = parameter.Name,
                        Description = parameter.Description,
                        Value = bool.Parse(parameter.DefaultValue)
                    };
                case StrategyParameter.STRING:
                    return new StringParameter()
                    {
                        Id = parameter.Id,
                        Name = parameter.Name,
                        Description = parameter.Description,
                        Value = parameter.DefaultValue
                    };
                case StrategyParameter.EXTERNAL_PARAMETER:
                    return new ExternalParameter()
                    {
                        Id = parameter.Id,
                        Name = parameter.Name,
                        Description = parameter.Description,
                        Value = parameter.DefaultValue
                    };
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Parses bid/ask parameter value
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">In case of bad value</exception>
        /// <param name="defaultValue">Value to parse</param>
        /// <returns>Parsed value.</returns>
        private static PriceType ParsePriceType(string defaultValue)
        {
            switch (defaultValue)
            {
                case StrategyParameter.PRICE_TYPE_ASK:
                    return PriceType.Ask;
                case StrategyParameter.PRICE_TYPE_BID:
                    return PriceType.Bid;
            }
            throw new ArgumentOutOfRangeException();
        }
    }
}
