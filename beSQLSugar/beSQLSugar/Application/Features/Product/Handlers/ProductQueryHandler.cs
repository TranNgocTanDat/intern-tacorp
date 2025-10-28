using beSQLSugar.Application.Dto.response.Product;
using beSQLSugar.Application.Features.Product.Queries;
using beSQLSugar.Application.Services.ProductServices;
using MediatR;

namespace beSQLSugar.Application.Features.Product.Handlers
{
    public class ProductQueryHandler :
        IRequestHandler<GetAllProductQuery, List<ProductResponse>>,
        IRequestHandler<GetProductByIdQuery, ProductResponse?>,
        IRequestHandler<GetProductBySlugQuery, ProductResponse?>,
        IRequestHandler<FilterProductQuery, List<ProductResponse>>,
        IRequestHandler<GetFeatureProductsQuery, List<ProductResponse>>,
        IRequestHandler<GetProductWithDetailsQuery, ProductResponse>
    {
        private readonly IProductService _productService;
        public ProductQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<ProductResponse?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            // Sử dụng service để lấy sản phẩm theo id
            return await _productService.GetProductByIdAsync(request.Id);

        }

        public async Task<List<ProductResponse>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            return await _productService.GetAllProductsAsync();
        }

        public async Task<ProductResponse?> Handle(GetProductBySlugQuery request, CancellationToken cancellationToken)
        {
            // Sử dụng service để lấy sản phẩm theo slug
            return await _productService.GetBySlugAsync(request.Slug);
        }

        public async Task<List<ProductResponse>> Handle(FilterProductQuery request, CancellationToken cancellationToken)
        {
            // Sử dụng service để lấy sản phẩm theo filter
            return await _productService.FilterProductsAsync(request.FilterRequest!);
        }
        public async Task<List<ProductResponse>> Handle(GetFeatureProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productService.GetFeaturedProductsAsync();
        }
        public async Task<ProductResponse> Handle(GetProductWithDetailsQuery request, CancellationToken cancellationToken)
        {
            var result = await _productService.GetProductWithDetailsAsync(request.Id);
            if (result == null)
                throw new InvalidOperationException($"Product with id {request.Id} not found.");
            return result;
        }

    }
}
