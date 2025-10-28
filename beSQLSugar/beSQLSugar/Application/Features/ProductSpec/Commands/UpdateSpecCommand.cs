using beSQLSugar.Application.Dto.request.ProductSpec;
using beSQLSugar.Application.Dto.response.ProductSpec;
using MediatR;

namespace beSQLSugar.Application.Features.ProductSpec.Commands
{
    public class UpdateSpecCommand : IRequest<ProductSpecResponse>
    {
        public int ProductId { get; set; }
        public int SpecId { get; set; }
        public ProductSpecRequest? Request { get; set; }
        public UpdateSpecCommand(int productId, int specId, ProductSpecRequest? request)
        {
            this.ProductId = productId;
            this.SpecId = specId;
            this.Request = request;
        }
    }
}
