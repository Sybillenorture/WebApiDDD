using FluentValidation;
using Lucca.ExpenseApp.Domain.Entities;

namespace Lucca.ExpenseApp.Application.Validators
{
    public class ExpenseValidator : AbstractValidator<Expense>
    {

        public ExpenseValidator()
        {

            RuleFor(e => e.Date)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now)
                .GreaterThanOrEqualTo(DateTime.Now.AddMonths(-3))
                .WithMessage("Expense date must be within the last 3 months and cannot be in the future.");

            RuleFor(e => e.Comment)
                .NotEmpty()
                .WithMessage("Comment is mandatory.");
        }
    }
}