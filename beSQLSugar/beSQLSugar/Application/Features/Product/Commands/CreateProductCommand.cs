using beSQLSugar.Application.Dto.request.Product;
using beSQLSugar.Application.Dto.response.Product;
using MediatR;
using System.Security.Claims;

namespace beSQLSugar.Application.Features.Product.Commands
{
    public class CreateProductCommand : IRequest<ProductResponse>
    {
        public ProductRequest? Request { get; set; }
        public CreateProductCommand(ProductRequest request)
        {
            Request = request;
        }
    }
}
