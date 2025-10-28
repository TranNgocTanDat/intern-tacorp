using AutoMapper;
using beSQLSugar.Application.Dto.request.ProductSpec;
using beSQLSugar.Application.Dto.response.ProductSpec;
using beSQLSugar.Infrastructure.Database.Enities;

namespace beSQLSugar.Application.Mapping
{
    public class ProductSpecProfile : Profile
    {
        public ProductSpecProfile()
        {
            CreateMap<ProductSpec, ProductSpecResponse>()
                .ForMember(dest => dest.ProductName,
                           opt => opt.MapFrom(src => src.Product != null ? src.Product.ProductName : string.Empty)); ;
            CreateMap<ProductSpecRequest, ProductSpec>().ForMember(dest => dest.ProductId, opt => opt.Ignore()); ;
        }
    }
}
