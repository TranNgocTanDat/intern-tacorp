using beSQLSugar.Application.DTO.response;
using beSQLSugar.Application.DTOs.request;
using MediatR;

namespace beSQLSugar.Application.Features.Category.Queries
{
    public class FilterCategoryQuery : IRequest<List<CategoryResponse>>
    {
        public CategoryFilterRequest? FilterRequest { get; set; }
        public FilterCategoryQuery(CategoryFilterRequest? filterRequest)
        {
            FilterRequest = filterRequest;
        }
    }
}
