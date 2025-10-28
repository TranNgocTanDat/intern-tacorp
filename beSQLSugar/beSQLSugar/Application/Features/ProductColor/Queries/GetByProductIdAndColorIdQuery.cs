using beSQLSugar.Application.Dto.response.ProductColor;
using MediatR;

namespace beSQLSugar.Application.Features.ProductColor.Queries
{
    public class GetByProductIdAndColorIdQuery : IRequest<List<ProductColorResponse>>
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public GetByProductIdAndColorIdQuery(int productId, int colorId)
        {
            ProductId = productId;
            ColorId = colorId;
        }
    }
}
