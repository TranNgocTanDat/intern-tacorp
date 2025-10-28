using beSQLSugar.Application.Dto.request.ProductSpec;
using beSQLSugar.Application.Dto.response.ProductSpec;
using MediatR;

namespace beSQLSugar.Application.Features.ProductSpec.Commands
{
    public class AddSpecCommand : IRequest<ProductSpecResponse>
    {
        public int ProductId { get; set; }
        public ProductSpecRequest? Request { get; set; }
        public AddSpecCommand(int productId, ProductSpecRequest? request)
        {
            ProductId = productId;
            Request = request;
        }
    }
   
}
