using beSQLSugar.Application.Dto.response.ProductStorage;
using beSQLSugar.Application.Features.ProductStorage.Queries;
using beSQLSugar.Application.Services.ProductStorageServices;
using MediatR;

namespace beSQLSugar.Application.Features.ProductStorage.Handlers
{
    public class ProductStorageQueryHandler :
            IRequestHandler<GetAllProductStorageQuery, List<ProductStorageResponse>>,
            IRequestHandler<GetByProductIdQuery, List<ProductStorageResponse>>,
            IRequestHandler<FilterProductStorageQuery, List<ProductStorageResponse>>
    {
        private readonly IProductStorageService _productStorageService;
        public ProductStorageQueryHandler(IProductStorageService productStorageService)
        {
            _productStorageService = productStorageService;
        }

        public async Task<List<ProductStorageResponse>> Handle(GetAllProductStorageQuery request, CancellationToken cancellationToken)
        {
            return await _productStorageService.GetAllWithProductAsync();
        }

        public async Task<List<ProductStorageResponse>> Handle(GetByProductIdQuery request, CancellationToken cancellationToken)
        {
            return await _productStorageService.GetByIdAsync(request.ProductId);
        }

        public async Task<List<ProductStorageResponse>> Handle(FilterProductStorageQuery request, CancellationToken cancellationToken)
        {
            return await _productStorageService.FilterProductStorageAsync(request.Request);
        }
    }
}
