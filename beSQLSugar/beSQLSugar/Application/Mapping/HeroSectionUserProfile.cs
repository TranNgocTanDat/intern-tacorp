using AutoMapper;
using beSQLSugar.Application.Dto.request.HeroSection;
using beSQLSugar.Application.Dto.response.HeroSection;
using beSQLSugar.Infrastructure.Database.Enities;

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
