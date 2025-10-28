using beSQLSugar.Application.Dto.response.ProductMedia;
using MediatR;

namespace beSQLSugar.Application.Features.ProductMedia.Queries
{
    public class GetMediaProductByIdQuery : IRequest<List<ProductMediaResponse>>
    {
        public int ProductId { get; set; }
        public GetMediaProductByIdQuery(int productId)
        {
            ProductId = productId;
        }
    }
}
