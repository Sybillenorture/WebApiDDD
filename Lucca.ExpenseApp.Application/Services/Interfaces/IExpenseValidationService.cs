using Lucca.ExpenseApp.Domain.Entities;

namespace Lucca.ExpenseApp.Application.Services.Interfaces
{
    public interface IExpenseValidationService
    {
        Task ValidateAsync(Expense expense);
    }
}
