using System.ComponentModel.DataAnnotations;

namespace LuccaExpenses.Api.Enums
{
    public enum Currency
    {
        [Display(Name = "USD")]
        USD = 1,

        [Display(Name = "RUB")]
        RUB = 2,

        [Display(Name = "EUR")]
        EUR = 3
    }
}
