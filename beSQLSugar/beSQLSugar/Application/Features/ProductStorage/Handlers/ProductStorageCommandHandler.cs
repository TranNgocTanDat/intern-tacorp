using beSQLSugar.Application.Dto.response.ProductStorage;
using beSQLSugar.Application.Features.ProductStorage.Commands;
using beSQLSugar.Application.Services.ProductStorageServices;
using MediatR;

namespace beSQLSugar.Application.Features.ProductStorage.Handlers
{
    // Fix: UpdateProductStorageCommand should return ProductStorageResponse, not ProductColorResponse.
    public class ProductStorageCommandHandler :
        IRequestHandler<CreateProductStorageCommand, ProductStorageResponse>,
        IRequestHandler<UpdateProductStorageCommand, ProductStorageResponse>,
        IRequestHandler<DeleteProductStorageCommand, bool>
    {
        private readonly IProductStorageService _productStorageService;
        public ProductStorageCommandHandler(IProductStorageService productStorageService)
        {
            _productStorageService = productStorageService;
        }

        public async Task<ProductStorageResponse> Handle(CreateProductStorageCommand request, CancellationToken cancellationToken)
        {
            return await _productStorageService.CreateAsync(request.Request);
        }

        public async Task<ProductStorageResponse> Handle(UpdateProductStorageCommand request, CancellationToken cancellationToken)
        {
            // Fix: Cast request.Request to ProductStorageRequest if necessary.
            return await _productStorageService.UpdateAsync(request.Id, request.Request!);
        }

        public async Task<bool> Handle(DeleteProductStorageCommand request, CancellationToken cancellationToken)
        {
            return await _productStorageService.DeleteAsync(request.Id);
        }
    }

    
}
