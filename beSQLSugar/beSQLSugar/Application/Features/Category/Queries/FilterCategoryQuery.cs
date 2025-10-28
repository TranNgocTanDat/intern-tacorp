using beSQLSugar.Application.Dto.request.Category;
using beSQLSugar.Application.Dto.response.Category;
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
