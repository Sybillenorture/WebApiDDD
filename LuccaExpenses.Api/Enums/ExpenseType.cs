using System.ComponentModel.DataAnnotations;

namespace LuccaExpenses.Api.Enums
{
    public enum ExpenseType
    {
        [Display(Name = "Restaurant")]
        Restaurant = 1,

        [Display(Name = "Hotel")]
        Hotel = 2,

        [Display(Name = "Misc")]
        Misc = 3
    }
}
