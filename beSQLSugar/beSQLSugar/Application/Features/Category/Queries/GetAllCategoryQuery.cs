using beSQLSugar.Application.DTO.response;
using MediatR;

namespace beSQLSugar.Application.Features.Category.Queries
{
    public class GetAllCategoryQuery : IRequest<List<CategoryResponse>>
    {
        public GetAllCategoryQuery() { }
    }
}
