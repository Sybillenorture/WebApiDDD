using FluentValidation;
using LuccaExpenses.Api.DTOs;

namespace LuccaExpenses.Api.Validators
{
    public class ExpenseValidator : AbstractValidator<ExpenseDto>
    {
        public ExpenseValidator()
        {
            RuleFor(x => x.Date)
                .LessThanOrEqualTo(DateTime.Today)
                .GreaterThanOrEqualTo(DateTime.Now.AddMonths(-3))
                .WithMessage("The expense date must be within the last 3 months.");

            RuleFor(x => x.Comment)
                .NotEmpty()
                .WithMessage("The comment is mandatory.");

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("The amount must be greater than zero.");
        }
    }
}
