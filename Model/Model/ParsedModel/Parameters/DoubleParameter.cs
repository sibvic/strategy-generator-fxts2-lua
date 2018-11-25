namespace ProfitRobots.StrategyGenerator.Model
{
    public class DoubleParameter : IParameter
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Value { get; set; }

        public ParameterType ParameterType => ParameterType.Double;
    }
}
