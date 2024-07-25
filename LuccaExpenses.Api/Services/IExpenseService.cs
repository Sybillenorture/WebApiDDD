using LuccaExpenses.Api.DTOs;
using LuccaExpenses.Api.Models;

namespace LuccaExpenses.Api.Services
{
    public interface IExpenseService
    {
        Task<Expense> CreateExpenseAsync(ExpenseDto dto);
        Task<IEnumerable<ExpenseDto>> GetExpensesAsync(int userId, string sortBy);
        Task<IEnumerable<ExpenseDto>> GetDuplicatedExpenses(ExpenseDto expenseDto);
        Task<string> ValidateExpense(ExpenseDto expenseDto);
    }
}
