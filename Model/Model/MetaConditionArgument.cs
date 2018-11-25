using System;
using ProfitRobots.StrategyGenerator.Model.ModelParser;

namespace ProfitRobots.StrategyGenerator.Model
{
    public enum FormulaItemType
    {
        Stream,
        Value,
        StreamValue,
        Operand,
        Parameter
    }

    public enum StreamType
    {
        NotUsed,
        Indicator,
        Instrument
    }

    public class MetaFormulaItem
    {
        public static MetaFormulaItem Create(FormulaItem argument)
        {
            if (argument == null)
                return null;

            return new MetaFormulaItem(argument);
        }

        public MetaFormulaItem()
        {
        }

        public MetaFormulaItem(FormulaItem arg)
        {
            Substream = arg.Substream;
            Value = arg.Value;
            PeriodShift = Util.ReplaceParameter(arg.PeriodShift);
            PeriodShiftSource = arg.PeriodShiftSource;
            switch (arg.ValueType)
            {
                case FormulaItem.STREAM_VALUE:
                    ValueType = FormulaItemType.StreamValue;
                    break;
                case FormulaItem.STREAM:
                    ValueType = FormulaItemType.Stream;
                    break;
                case FormulaItem.VALUE:
                    ValueType = FormulaItemType.Value;
                    break;
                case FormulaItem.OPERAND:
                    ValueType = FormulaItemType.Operand;
                    break;
                case FormulaItem.PARAM:
                    ValueType = FormulaItemType.Parameter;
                    break;
                default:
                    throw new NotImplementedException();
            }

            switch (arg.StreamType)
            {
                case FormulaItem.NOT_USED:
                    StreamType = ValueType == FormulaItemType.Stream ? StreamType.Indicator : StreamType.NotUsed;
                    break;
                case FormulaItem.INDICATOR:
                    StreamType = StreamType.Indicator;
                    break;
                case FormulaItem.INSTRUMENT:
                    StreamType = StreamType.Instrument;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
       
        /// <summary>
        /// Type of the stream.
        /// </summary>
        public StreamType StreamType { get; set; }

        /// <summary>
        /// Contains either the value either the name of the stream.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Type of the value.
        /// </summary>
        public FormulaItemType ValueType { get; set; }

        /// <summary>
        /// If the Value is a stream this field may contain name of the substream (like "close")
        /// </summary>
        public string Substream { get; set; }

        /// <summary>
        /// Period shift for the value stream. The latest value will be used if empty.
        /// </summary>
        public string PeriodShift { get; set; }

        /// <summary>
        /// Period shift source. 
        /// Period could be shifted on another timeframe and/or source (which could have skipped bars).
        /// </summary>
        public string PeriodShiftSource { get; set; }
    }
}
