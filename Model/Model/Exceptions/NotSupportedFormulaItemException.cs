namespace ProfitRobots.StrategyGenerator.Model
{
    public class NotSupportedFormulaItemException : StrategyGeneratorException
    {
        public NotSupportedFormulaItemException(FormulaItemType formulaItem, string language)
            :base($"Use of {ItemToString(formulaItem)} is not supported in formula for {language} yet")
        {

        }

        private static string ItemToString(FormulaItemType formulaItem)
        {
            switch (formulaItem)
            {
                case FormulaItemType.Parameter:
                    return "parameter";
                case FormulaItemType.Stream:
                    return "stream";
                case FormulaItemType.StreamValue:
                    return "stream value";
                case FormulaItemType.Value:
                    return "value";
            }
            return "unknown";
        }
    }
}
