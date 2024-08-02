using Lucca.ExpenseApp.Domain.Entities;
using Lucca.ExpenseApp.Dto;

namespace Lucca.ExpenseApp.Application.Services.Interfaces
{
    public interface IExpenseService
    {
        Task<IEnumerable<ExpenseResponse>> ListUserExpensesAsync(int userId, string sortBy);
        Task<ExpenseResponse> AddAsync(Expense expense);
    }
}
