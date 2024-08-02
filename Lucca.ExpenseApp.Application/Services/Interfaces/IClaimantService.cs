using Lucca.ExpenseApp.Dto;

namespace Lucca.ExpenseApp.Application.Services.Interfaces
{
    public interface IClaimantService
    {
        Task<ClaimantResponse> GetByIdAsync(int id);
    }
}
