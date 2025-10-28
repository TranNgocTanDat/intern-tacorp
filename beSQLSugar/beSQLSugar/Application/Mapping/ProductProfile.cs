using AutoMapper;
using beSQLSugar.Application.Dto.request.Product;
using beSQLSugar.Application.Dto.response.Product;
using beSQLSugar.Infrastructure.Database.Enities;

namespace beSQLSugar.Application.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResponse>()
               // Map nested MediaList
               .ForMember(dest => dest.MediaList,
                          opt => opt.MapFrom(src => src.MediaList))
               // Map nested Specs
               .ForMember(dest => dest.Specs,
                          opt => opt.MapFrom(src => src.Specs))
                .ForMember(dest => dest.Colors,
                           opt => opt.MapFrom(src => src.Colors))
                .ForMember(dest => dest.Storages,
                           opt => opt.MapFrom(src => src.Storages))
               .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null)); ;

            CreateMap<ProductRequest, Product>();
        }
    }
}
