using AutoMapper;
using JasperFx.Core;
using LuccaExpenses.Api.DTOs;
using LuccaExpenses.Api.Enums;
using LuccaExpenses.Api.Models;

namespace LuccaExpenses.Api.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(des => des.Currency, opt => opt.MapFrom(src => (Currency)Enum.Parse(typeof(Currency), src.Currency)))
                .ReverseMap()
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency.ToString()));

        }        
    }
}
