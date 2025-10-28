using beSQLSugar.Application.Dto.request.Product;
using beSQLSugar.Application.Dto.response.Product;
using MediatR;
using System.Security.Claims;

namespace beSQLSugar.Application.Features.Product.Commands
{
    public class UpdateProductCommand : IRequest<ProductResponse>
    {
        public int Id { get; set; }
        public ProductRequest Request { get; set; }
        public UpdateProductCommand(int id, ProductRequest request)
        {
            Id = id;
            Request = request;

        }
    }
}
