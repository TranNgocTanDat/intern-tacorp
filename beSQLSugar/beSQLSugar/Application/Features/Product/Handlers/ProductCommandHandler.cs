using beSQLSugar.Application.Dto.response.Product;
using beSQLSugar.Application.Features.Product.Commands;

using beSQLSugar.Application.Services.ProductServices;
using MediatR;

namespace beSQLSugar.Application.Features.Product.Handlers
{
    public class ProductCommandHandler :
        IRequestHandler<CreateProductCommand, ProductResponse>,
        IRequestHandler<UpdateProductCommand, ProductResponse>,
        IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductService _productService;
        public ProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        // Triển khai phương thức Handle cho CreateProductCommand
        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // Gọi dịch vụ để thêm sản phẩm mới
            var productResponse = await _productService.AddProductAsync(request.Request!);
            if (productResponse == null)
            {
                throw new Exception("Failed to create product");
            }
            return productResponse;
        }

        public async Task<ProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            // Gọi dịch vụ để cập nhật sản phẩm
            var productResponse = await _productService.UpdateProductAsync(request.Id, request.Request!);
            if (productResponse == null)
            {
                throw new Exception("Failed to update product");
            }
            return productResponse;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            return await _productService.DeleteProductAsync(request.Id);
        }

        
    }
}
