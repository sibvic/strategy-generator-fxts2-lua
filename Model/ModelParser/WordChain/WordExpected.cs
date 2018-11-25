namespace ProfitRobots.StrategyGenerator.ModelParser.WordChain
{
    /// <summary>
    /// Expects the word
    /// </summary>
    public class WordExpected : IExpectedItem
    {
        private readonly string _expected;
        private readonly bool _optional;

        public WordExpected(string extected, bool optional = false)
        {
            _expected = extected.ToLower();
            _optional = optional;
        }

        public (string name, object value) Result => (null, null);

        public bool IsOptional => _optional;

        public bool Parse(string word)
        {
            return _expected == word.ToLower();
        }

        public void Reset()
        {
        }
    }
}
