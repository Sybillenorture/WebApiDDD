using Lucca.ExpenseApp.Domain.Entities;
using Lucca.ExpenseApp.Domain.Interfaces.Repositories;
using Lucca.ExpenseApp.Infrastructure.Persistence;

namespace Lucca.ExpenseApp.Infrastructure.Repositories
{
    public class ClaimantRepository : IClaimantRepository
    {
        private readonly ExpenseAppDbContext _context;

        public ClaimantRepository(ExpenseAppDbContext context)
        {
            _context = context;
        }

        public async Task<Claimant> GetByIdAsync(int userId)
        {
            return await _context.Claimants.FindAsync(userId);
        }
    }
}