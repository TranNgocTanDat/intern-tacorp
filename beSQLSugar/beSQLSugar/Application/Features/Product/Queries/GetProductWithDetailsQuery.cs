using beSQLSugar.Application.Dto.response.Product;
using MediatR;

namespace beSQLSugar.Application.Features.Product.Queries
{
    public class GetProductWithDetailsQuery : IRequest<ProductResponse>
    {
        public int Id { get; set; }
        public GetProductWithDetailsQuery(int id)
        {
            Id = id;
        }
    }
}
