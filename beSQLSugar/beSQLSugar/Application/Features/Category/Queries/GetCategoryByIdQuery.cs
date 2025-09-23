using beSQLSugar.Application.DTO.response;
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
