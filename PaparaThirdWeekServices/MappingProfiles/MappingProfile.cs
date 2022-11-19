using AutoMapper;
using PaparaThirdWeek.Domain.Entities;
using PaparaThirdWeek.Services.DTOs;

namespace PaparaThirdWeek.Services.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>().ReverseMap();            
        }
    }
}
