namespace ProfitRobots.StrategyGenerator.Model
{
    /// <summary>
    /// String parameter.
    /// </summary>
    public class StringParameter : IParameter
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ParameterType ParameterType => ParameterType.String;

        public string Value { get; set; }
    }
}
