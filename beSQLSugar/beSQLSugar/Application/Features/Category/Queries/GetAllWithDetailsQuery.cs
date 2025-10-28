using beSQLSugar.Application.Dto.response.Category;
using MediatR;

namespace beSQLSugar.Application.Features.Category.Queries
{
    public class GetAllWithDetailsQuery : IRequest<List<CategoryResponse>>
    {
        public GetAllWithDetailsQuery() { }
    }
}
