using beSQLSugar.Application.Dto.response.Category;
using MediatR;

namespace beSQLSugar.Application.Features.Category.Queries
{
    public class GetCategoryChildrenQuery : IRequest<List<CategoryResponse>>
    {
        public GetCategoryChildrenQuery() { }
    }
}
