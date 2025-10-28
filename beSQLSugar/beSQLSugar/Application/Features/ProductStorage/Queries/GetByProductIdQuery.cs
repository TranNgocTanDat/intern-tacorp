using beSQLSugar.Application.Dto.response.ProductStorage;
using MediatR;

namespace beSQLSugar.Application.Features.ProductStorage.Queries
{
    public class GetByProductIdQuery : IRequest<List<ProductStorageResponse>>
    {
        public int ProductId { get; set; }
        public GetByProductIdQuery(int productId)
        {
            ProductId = productId;
        }
    }
}
