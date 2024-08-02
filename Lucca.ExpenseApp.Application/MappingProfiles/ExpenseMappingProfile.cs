using AutoMapper;
using Lucca.ExpenseApp.Domain.Entities;
using Lucca.ExpenseApp.Dto;

namespace Lucca.ExpenseApp.Application.MappingProfiles
{
    public class ExpenseMappingProfile : Profile
    {
        public ExpenseMappingProfile()
        {
            CreateMap<Expense, ExpenseResponse>()
               .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString())) // Convert Enum to String
               .ForMember(dest => dest.ClaimantFullName, opt =>
                   opt.MapFrom(src => $"{src.Claimant.FirstName} {src.Claimant.LastName}")); // Format FullName
        }  
    }
}
