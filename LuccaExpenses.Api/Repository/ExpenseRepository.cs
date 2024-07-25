
using LuccaExpenses.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace LuccaExpenses.Api.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly LuccaExpensesDbContext _context;

        public ExpenseRepository(LuccaExpensesDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Expense expense)
        {
            await _context.Expense.AddAsync(expense);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Check if an expense have already been created
        /// </summary>
        /// <param name="userId">if of the user that create the expense</param>
        /// <param name="date">datetime</param>
        /// <param name="amount">amount of the expense</param>
        /// <returns>return the list of expenses found that match the params or null if not</returns>
        public async Task<IEnumerable<Expense>> GetDuplicatedExpenseRecords(int userId, DateTime date, decimal amount)
        {
            return await _context.Expense.Where(e => e.UserId == userId && e.Date.Date == date.Date && e.Amount == amount).ToListAsync();
        }

        /// <summary>
        /// Get a list of Expense given a user
        /// </summary>
        /// <param name="userId">id of the user</param>
        /// <returns>list of Expense</returns>
        public async Task<List<Expense>> GetByUserIdAsync(int userId)
        {
            return await _context.Expense.Where(e => e.UserId == userId).ToListAsync();
        }
    }
}
