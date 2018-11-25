namespace ProfitRobots.StrategyGenerator.Model
{
    public enum ParameterType
    {
        Integer,
        PriceType,
        Timeframe,
        Boolean,
        String,
        Price,
        Double,
        External
    }

    public interface IParameter
    {
        string Id { get; }

        string Name { get; }

        string Description { get; }

        ParameterType ParameterType { get; }
    }
}
