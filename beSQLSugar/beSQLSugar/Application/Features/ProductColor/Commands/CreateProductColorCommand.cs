using beSQLSugar.Application.Dto.request.ProductColor;
using beSQLSugar.Application.Dto.response.ProductColor;
using MediatR;

namespace beSQLSugar.Application.Features.ProductColor.Commands
{
    public class CreateProductColorCommand : IRequest<ProductColorResponse>
    {
        public ProductColorRequest Request { get; set; } 
        public CreateProductColorCommand(ProductColorRequest request)
        {
            Request = request;
        }
    }
}
