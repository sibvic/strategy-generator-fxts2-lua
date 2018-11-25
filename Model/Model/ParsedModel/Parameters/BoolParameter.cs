namespace ProfitRobots.StrategyGenerator.Model
{
    /// <summary>
    /// Boolean parameter
    /// </summary>
    public class BoolParameter : IParameter
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ParameterType ParameterType => ParameterType.Boolean;

        public bool Value { get; set; }
    }
}
