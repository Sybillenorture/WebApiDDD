using Lucca.ExpenseApp.Domain.Entities;

namespace Lucca.ExpenseApp.Domain.Interfaces.Repositories
{
    public interface IClaimantRepository
    {
        Task<Claimant> GetByIdAsync(int userId);
    }
}
