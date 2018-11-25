using System.ComponentModel.DataAnnotations;

namespace ProfitRobots.StrategyGenerator.Model
{
    public enum MetaActionType
    {
        [Display(Name = "Customizable")]
        Customizable,
        [Display(Name = "Buy")]
        Buy,
        [Display(Name = "Sell")]
        Sell,
        [Display(Name = "Exit")]
        Exit,
        [Display(Name = "Exit Buy")]
        ExitBuy,
        [Display(Name = "Exit Sell")]
        ExitSell,
        [Display(Name = "Entry Buy")]
        EntryBuy,
        [Display(Name = "Entry Sell")]
        EntrySell,
    }
}
