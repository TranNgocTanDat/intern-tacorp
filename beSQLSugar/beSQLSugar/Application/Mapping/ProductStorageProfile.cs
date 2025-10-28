using AutoMapper;
using beSQLSugar.Application.Dto.request.ProductStorage;
using beSQLSugar.Application.Dto.response.ProductStorage;
using beSQLSugar.Infrastructure.Database.Enities;
using Microsoft.Identity.Client.Extensions.Msal;

namespace beSQLSugar.Application.Mapping
{
    public class ProductStorageProfile : Profile
    {
        public ProductStorageProfile()
        {
            CreateMap<ProductStorage, ProductStorageResponse>()
                .ForMember(dest => dest.ProductName,
                           opt => opt.MapFrom(src => src.Product != null ? src.Product.ProductName : null));

            CreateMap<ProductStorageRequest, ProductStorage>()
                .ForMember(dest => dest.ProductId, opt => opt.Ignore());
        }
    }
}
