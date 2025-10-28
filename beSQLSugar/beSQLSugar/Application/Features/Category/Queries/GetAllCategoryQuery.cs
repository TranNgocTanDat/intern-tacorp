using beSQLSugar.Application.Dto.response.Category;
using MediatR;

namespace beSQLSugar.Application.Features.Category.Queries
{
    public class GetAllCategoryQuery : IRequest<List<CategoryResponse>>
    {
        public GetAllCategoryQuery() { }
    }
}
