using FluentValidation;
using Lucca.ExpenseApp.Domain.Entities;

namespace Lucca.ExpenseApp.Application.Validators
{
    public class ClaimantValidator : AbstractValidator<Claimant>
    {
        public ClaimantValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty();
            RuleFor(u => u.LastName).NotEmpty();
            RuleFor(u => u.Currency).NotEmpty();
            // More validation rules can be added here
        }
    }
}