using LuccaExpenses.Api.Models;

namespace LuccaExpenses.Api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly LuccaExpensesDbContext _context;

        public UserRepository(LuccaExpensesDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a user
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>User</returns>
        public async Task<User?> GetByIdAsync(int userId)
        {
            return await _context.User.FindAsync(userId);

        }
    }
}
