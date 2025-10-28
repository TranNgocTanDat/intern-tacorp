using beSQLSugar.Application.Dto.request.ProductColor;
using beSQLSugar.Application.Dto.request.ProductStorage;
using beSQLSugar.Application.Dto.response.ProductColor;
using beSQLSugar.Application.Dto.response.ProductStorage;
using MediatR;

namespace beSQLSugar.Application.Features.ProductStorage.Commands
{
    public class UpdateProductStorageCommand : IRequest<ProductStorageResponse>
    {
        public int Id { get; set; }
        public ProductStorageRequest Request { get; set; } 
        public UpdateProductStorageCommand(int id, ProductStorageRequest request)
        {
            Id = id;
            Request = request;
        }
    }
}
