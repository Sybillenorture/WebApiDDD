using Lucca.ExpenseApp.Domain.Entities;
using Lucca.ExpenseApp.Domain.Interfaces.Repositories;
using Lucca.ExpenseApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Lucca.ExpenseApp.Infrastructure.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ExpenseAppDbContext _context;

        public ExpenseRepository(ExpenseAppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Expense>> GetClaimantExpensesAsync(int userId)
        {
            return await _context.Expenses.Where(e => e.Claimant.Id == userId).ToListAsync();
        }

        public async Task<bool> ExpenseExistsAsync(int userId, DateTime date, decimal amount)
        {
            return await _context.Expenses.AnyAsync(e => e.Claimant.Id == userId && e.Date == date && e.Amount == amount);
        }

        public async Task<Expense> GetExpenseByIdAsync(int expenseId)
        {
            return await _context.Expenses
                .Include(e => e.Claimant)  // Assurez-vous d'inclure les relations nécessaires
                .FirstOrDefaultAsync(e => e.Id == expenseId);
        }
    }
}
