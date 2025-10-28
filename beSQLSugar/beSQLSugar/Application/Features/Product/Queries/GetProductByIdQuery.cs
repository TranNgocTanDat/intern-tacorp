using beSQLSugar.Application.Dto.response.Product;
using MediatR;

namespace beSQLSugar.Application.Features.Product.Queries
{
    public class GetProductByIdQuery : IRequest<ProductResponse>
    {
        public int Id { get; set; }
        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }
}
