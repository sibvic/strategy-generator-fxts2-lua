namespace ProfitRobots.StrategyGenerator.Model
{
    public class IntParameter : IParameter
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Value { get; set; }

        public ParameterType ParameterType => ParameterType.Integer;
    }
}
