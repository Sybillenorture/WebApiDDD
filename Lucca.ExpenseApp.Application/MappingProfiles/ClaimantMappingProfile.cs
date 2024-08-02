using AutoMapper;
using Lucca.ExpenseApp.Domain.Entities;
using Lucca.ExpenseApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucca.ExpenseApp.Application.MappingProfiles
{
    public class ClaimantMappingProfile : Profile
    {
        public ClaimantMappingProfile()
        {
            CreateMap<Claimant, ClaimantResponse>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency))
                .ReverseMap();
        }
    }
}
