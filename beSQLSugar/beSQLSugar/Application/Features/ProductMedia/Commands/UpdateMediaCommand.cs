using beSQLSugar.Application.Dto.request.ProductMedia;
using beSQLSugar.Application.Dto.response.ProductMedia;
using MediatR;

namespace beSQLSugar.Application.Features.ProductMedia.Commands
{
    public class UpdateMediaCommand : IRequest<ProductMediaResponse>
    {
        public int ProductId { get; set; }
        public int MediaId { get; set; }
        public ProductMediaRequest Request { get; set; }

        public UpdateMediaCommand(int productId, int mediaId, ProductMediaRequest request)
        {
            ProductId = productId;
            MediaId = mediaId;
            Request = request;
        }
    }
}
