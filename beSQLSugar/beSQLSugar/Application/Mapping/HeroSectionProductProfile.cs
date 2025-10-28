using AutoMapper;
using beSQLSugar.Application.Dto.request.HeroSectionProduct;
using beSQLSugar.Application.Dto.response.HeroSectionProduct;
using beSQLSugar.Infrastructure.Database.Enities;

namespace beSQLSugar.Application.Mapping
{
    public class HeroSectionProductProfile : Profile
    {
        public HeroSectionProductProfile()
        {
            // Map từ Request -> Entity
            CreateMap<HeroSectionProductRequest, HeroSectionProduct>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // bỏ qua Id khi map từ request
                .ForMember(dest => dest.HeroSection, opt => opt.Ignore()) // chỉ map Id, không map navigation
                .ForMember(dest => dest.Product, opt => opt.Ignore());

            CreateMap<HeroSectionProduct, HeroSectionProductResponse>()
                .ForMember(dest => dest.HeroSection, opt => opt.MapFrom(src => src.HeroSection))
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));

        }

    }
}
