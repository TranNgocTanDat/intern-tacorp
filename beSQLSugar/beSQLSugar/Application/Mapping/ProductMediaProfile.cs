using AutoMapper;
using beSQLSugar.Application.Dto.request.ProductMedia;
using beSQLSugar.Application.Dto.response.ProductMedia;
using beSQLSugar.Infrastructure.Database.Enities;

namespace beSQLSugar.Application.Mapping
{
    public class ProductMediaProfile : Profile
    {
        public ProductMediaProfile()
        {
            CreateMap<ProductMedia, ProductMediaResponse>()
                .ForMember(dest => dest.ProductName,
                           opt => opt.MapFrom(src => src.Product != null ? src.Product.ProductName : string.Empty))
                .ForMember(dest => dest.ColorName,
                           opt => opt.MapFrom(src => src.ProductColor != null ? src.ProductColor.ColorName : string.Empty))
            .ForMember(dest => dest.ColorCode,
                           opt => opt.MapFrom(src => src.ProductColor != null ? src.ProductColor.ColorCode : string.Empty));
            CreateMap<ProductMediaRequest, ProductMedia>()
                .ForMember(dest => dest.ProductId, opt => opt.Ignore()) // Không cho phép override ProductId
                .ForMember(dest => dest.MediaFileUrl,
                           opt => opt.Condition(src => src.MediaFileUrl != null));// Chỉ map khi có file mới
        }
    
    }
}
