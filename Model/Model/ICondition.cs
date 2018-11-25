namespace ProfitRobots.StrategyGenerator.Model
{
    public enum ConditionType
    {
        Crosses,
        CrossesOver,
        CrossesOverOrTouch,
        CrossesUnder,
        CrossesUnderOrTouch,
        Or,
        And,
        Greater,
        Lesser,
        GreaterOrEqual,
        LesserOrEqual,
        Equal,
        NotEqual
    }

    public interface ICondition
    {
        ConditionType ConditionType { get; }
    }
}
