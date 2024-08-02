using FluentValidation;
using Lucca.ExpenseApp.Application.Services.Interfaces;
using Lucca.ExpenseApp.Domain.Entities;
using Lucca.ExpenseApp.Domain.Interfaces.Repositories;

namespace Lucca.ExpenseApp.Application.Services.Concrete
{
    public class ExpenseValidationService : IExpenseValidationService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IValidator<Expense> _expenseValidator;
        private readonly IValidator<Claimant> _claimantValidator;

        public ExpenseValidationService(
            IExpenseRepository expenseRepository,
            IValidator<Expense> expenseValidator,
            IValidator<Claimant> claimantValidator)
        {
            _expenseRepository = expenseRepository;
            _expenseValidator = expenseValidator;
            _claimantValidator = claimantValidator;
        }

        public async Task ValidateAsync(Expense expense)
        {
            var expenseValidationResult = await _expenseValidator.ValidateAsync(expense);
            if (!expenseValidationResult.IsValid)
            {
                throw new ValidationException(expenseValidationResult.Errors);
            }

            var userValidationResult = await _claimantValidator.ValidateAsync(expense.Claimant);
            if (!userValidationResult.IsValid)
            {
                throw new ValidationException(userValidationResult.Errors);
            }

            if (expense.Currency != expense.Claimant.Currency)
            {
                throw new ValidationException("Expense currency must match user's currency");
            }

            if (await _expenseRepository.ExpenseExistsAsync(expense.Claimant.Id, expense.Date, expense.Amount))
            {
                throw new ValidationException("Expense with the same date and amount already exists");
            }
        }
    }
}