using AutoMapper;
using beSQLSugar.Application.DTO.request;
using beSQLSugar.Application.DTO.response;
using beSQLSugar.Domain.Enities;

namespace beSQLSugar.Application.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            // Map từ Entity -> Response
            CreateMap<Category, CategoryResponse>()
                .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

            // Map từ Request -> Entity
            CreateMap<CategoryRequest, Category>();
                
        }
    }
}
