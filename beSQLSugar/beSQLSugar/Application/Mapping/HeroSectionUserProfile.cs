using AutoMapper;
using beSQLSugar.Application.DTO.request;
using beSQLSugar.Application.DTO.response;
using beSQLSugar.Domain.Enities;

namespace beSQLSugar.Application.Mapping
{
    public class HeroSectionUserProfile : Profile
    {
        public HeroSectionUserProfile()
        {
            // Từ HeroSection sang HeroSectionResponse
            CreateMap<HeroSection, HeroSectionResponse>()
                .ForMember(dest => dest.HeroProducts, opt => opt.MapFrom(src => src.HeroProducts));
            // Từ HeroSectionRequest sang HeroSection
            CreateMap<HeroSectionRequest, HeroSection>();
        }
    }
}
