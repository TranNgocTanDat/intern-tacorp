using beSQLSugar.Application.Dto.request.ProductStorage;
using beSQLSugar.Application.Dto.response.ProductStorage;
using MediatR;
using Microsoft.Identity.Client;

namespace beSQLSugar.Application.Features.ProductStorage.Commands
{
    public class CreateProductStorageCommand : IRequest<ProductStorageResponse>
    {
        public ProductStorageRequest Request { get; set; }
        public CreateProductStorageCommand(ProductStorageRequest request)
        {
            Request = request;
        }
    }
}
