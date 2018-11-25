using System.Text.RegularExpressions;

namespace ProfitRobots.StrategyGenerator.ModelParser.WordChain
{
    public class RegexWordExpected : IExpectedItem
    {
        private readonly Regex _expected;
        private readonly string _name;
        private string _value;

        public RegexWordExpected(Regex extected, string name = null)
        {
            _expected = extected;
            _name = name;
        }

        public (string name, object value) Result => (_name, _value);

        public bool IsOptional => false;

        public bool Parse(string word)
        {
            if (_expected.IsMatch(word.ToLower()))
            {
                _value = word;
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
