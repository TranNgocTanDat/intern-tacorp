using beSQLSugar.Application.Dto.response.Category;
using MediatR;

namespace beSQLSugar.Application.Features.Category.Queries
{
    public class GetCategoryWithDetailsQuery : IRequest<CategoryResponse>
    {
        public int Id { get; set; }
        public GetCategoryWithDetailsQuery(int id)
        {
            Id = id;
        }
    }
}
