using Lucca.ExpenseApp.Domain.Entities;

namespace Lucca.ExpenseApp.Domain.Interfaces.Repositories
{
    public interface IExpenseRepository
    {
        Task AddAsync(Expense expense);
        
        Task<IEnumerable<Expense>> GetClaimantExpensesAsync(int ClaimantId);
        
        Task<bool> ExpenseExistsAsync(int ClaimantId, DateTime date, decimal amount);
    }
}
