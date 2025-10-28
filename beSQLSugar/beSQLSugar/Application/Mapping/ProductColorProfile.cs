using AutoMapper;
using beSQLSugar.Application.Dto.request.ProductColor;
using beSQLSugar.Application.Dto.response.ProductColor;
using beSQLSugar.Infrastructure.Database.Enities;
using System.Drawing;

namespace beSQLSugar.Application.Mapping
{
    public class ProductColorProfile : Profile
    {
        public ProductColorProfile()
        {
            CreateMap<ProductColor, ProductColorResponse>()
                .ForMember(dest => dest.ProductName,
                           opt => opt.MapFrom(src => src.Product != null ? src.Product.ProductName : null));

            CreateMap<ProductColorRequest, ProductColor>()
                .ForMember(dest => dest.ProductId, opt => opt.Ignore());
        }
    }
}
