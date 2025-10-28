using beSQLSugar.Application.Dto.request.ProductMedia;
using beSQLSugar.Application.Dto.response.ProductMedia;
using MediatR;

namespace beSQLSugar.Application.Features.ProductMedia.Commands
{
    public class AddMediaCommand : IRequest<ProductMediaResponse>
    {
        public int ProductId { get; set; }
        public ProductMediaRequest? Request { get; set; }
        public AddMediaCommand(int productId, ProductMediaRequest? request)
        {
            ProductId = productId;
            Request = request;
        }
    }
    
}
