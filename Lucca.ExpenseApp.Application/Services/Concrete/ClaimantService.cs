using AutoMapper;
using Lucca.ExpenseApp.Application.Services.Interfaces;
using Lucca.ExpenseApp.Domain.Interfaces.Repositories;
using Lucca.ExpenseApp.Dto;

namespace Lucca.ExpenseApp.Application.Services.Concrete
{
    public class ClaimantService : IClaimantService
    {
        private readonly IMapper _mapper;
        private readonly IClaimantRepository _claimantRepository;

        public ClaimantService(IMapper mapper, IClaimantRepository claimantRepository)
        {
            _mapper = mapper;
            _claimantRepository = claimantRepository;
        }

        public async Task<ClaimantResponse> GetByIdAsync(int id)
        {
            var claimant = await _claimantRepository.GetByIdAsync(id);
            if (claimant == null)
            {
                return null;
            }
            return _mapper.Map<ClaimantResponse>(claimant);
        }
    }
}
