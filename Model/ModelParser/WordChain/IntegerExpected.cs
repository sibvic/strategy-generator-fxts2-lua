namespace ProfitRobots.StrategyGenerator.ModelParser.WordChain
{
    /// <summary>
    /// Expects an integer
    /// </summary>
    public class IntegerExpected : IExpectedItem
    {
        public IntegerExpected(string name)
        {
            _name = name;
        }

        int? _value;
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
            if (int.TryParse(word, out int value))
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
