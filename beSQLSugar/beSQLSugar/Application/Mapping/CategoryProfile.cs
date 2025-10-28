using AutoMapper;
using beSQLSugar.Application.Dto.request.Category;
using beSQLSugar.Application.Dto.response.Category;
using beSQLSugar.Infrastructure.Database.Enities;

namespace beSQLSugar.Application.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            // Map từ Entity -> Response
            CreateMap<Category, CategoryResponse>()
                .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products))
                .ForMember(dest => dest.ParentName,
           opt => opt.MapFrom(src => src.Parent != null ? src.Parent.Name : null))
                .ForMember(dest => dest.Partner, opt => opt.MapFrom(src => src.Partner));

            // Map từ Request -> Entity
            CreateMap<CategoryRequest, Category>();
                
        }
    }
}
