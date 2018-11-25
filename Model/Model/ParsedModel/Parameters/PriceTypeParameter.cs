namespace ProfitRobots.StrategyGenerator.Model
{
    public enum PriceType
    {
        Bid,
        Ask
    }

    public class PriceTypeParameter : IParameter
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public PriceType Value { get; set; }

        public ParameterType ParameterType => ParameterType.PriceType;
    }
}
