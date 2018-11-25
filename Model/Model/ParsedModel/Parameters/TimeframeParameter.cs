namespace ProfitRobots.StrategyGenerator.Model
{
    public class TimeframeParameter : IParameter
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Value { get; set; }

        public ParameterType ParameterType => ParameterType.Timeframe;
    }
}
