using beSQLSugar.Application.Dto.response.ProductSpec;
using MediatR;

namespace beSQLSugar.Application.Features.ProductSpec.Queries
{
    public class GetSpecProductByIdQuery : IRequest<List<ProductSpecResponse>>
    {
        public int ProductId { get; set; }
        public GetSpecProductByIdQuery(int productId)
        {
            ProductId = productId;
        }
    }
}
