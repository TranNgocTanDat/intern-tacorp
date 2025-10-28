using beSQLSugar.Application.Dto.response.Category;
using MediatR;

namespace beSQLSugar.Application.Features.Category.Queries
{
    public class GetCategoryByIdQuery : IRequest<CategoryResponse>  
    {
        public int Id { get; set; }
        public GetCategoryByIdQuery(int id)
        {
            Id = id;
        }
    }
}
