using beSQLSugar.Application.Dto.response.ProductColor;
using MediatR;

namespace beSQLSugar.Application.Features.ProductColor.Queries
{
    public class GetByProductIdQuery : IRequest<List<ProductColorResponse>>
    {
        public int ProductId { get; set; }
        public GetByProductIdQuery(int productId)
        {
            ProductId = productId;
        }
    }
}
