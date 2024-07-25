using LuccaExpenses.Api.DTOs;
using LuccaExpenses.Api.Models;

namespace LuccaExpenses.Api.Repository
{
    public interface IExpenseRepository
    {
        Task AddAsync(Expense expense);
        Task<List<Expense>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Expense>> GetDuplicatedExpenseRecords(int userId, DateTime date, decimal amount);
    }
}
