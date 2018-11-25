using System;
using System.Globalization;
using System.Text;

namespace ProfitRobots.StrategyGenerator.ModelParser.WordChain
{
    /// <summary>
    /// Expets a double
    /// </summary>
    public class DoubleExpected : IExpectedItem
    {
        public DoubleExpected(string name)
        {
            _name = name;
        }

        double? _value;
        private readonly string _name;

        public (string name, object value) Result
        {
            get
            {
                if (!_value.HasValue)
                {
                    return (null, null);
                }
                return (_name, _value.Value);
            }
        }

        public bool IsOptional => false;

        public bool Parse(string word)
        {
            if (double.TryParse(word.Replace(",", "."), NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
            {
                _value = value;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            _value = null;
        }
    }
}
