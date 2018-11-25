namespace ProfitRobots.StrategyGenerator.Model
{
    /// <summary>
    /// Reference to another parameter.
    /// </summary>
    public class ExternalParameter : IParameter
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Value { get; set; }

        public ParameterType ParameterType => ParameterType.External;
    }
}
