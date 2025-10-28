using beSQLSugar.Application.Dto.response.ProductColor;
using beSQLSugar.Application.Features.ProductColor.Queries;
using beSQLSugar.Application.Services.ProductColorServices;
using MediatR;

namespace beSQLSugar.Application.Features.ProductColor.Handlers
{
    public class ProductColorQueryHandler :
        IRequestHandler<GetAllProducColorQuery, List<ProductColorResponse>>,
        IRequestHandler<GetByProductIdQuery, List<ProductColorResponse>>,
        IRequestHandler<GetByProductIdAndColorIdQuery, List<ProductColorResponse>>,
        IRequestHandler<FilterProductColorQuery, List<ProductColorResponse>>
    {
        private readonly IProductColorService _productColorService;
        public ProductColorQueryHandler(IProductColorService productColorService)
        {
            _productColorService = productColorService;
        }

        public async Task<List<ProductColorResponse>> Handle(GetAllProducColorQuery request, CancellationToken cancellationToken)
        {
            return await _productColorService.GetAllAsync();
        }
        public async Task<List<ProductColorResponse>> Handle(GetByProductIdQuery request, CancellationToken cancellationToken)
        {
            return await _productColorService.GetByProductIdAsync(request.ProductId);

        }

        public async Task<List<ProductColorResponse>> Handle(GetByProductIdAndColorIdQuery request, CancellationToken cancellationToken)
        {
            return await _productColorService.GetByProductIdAndColorIdAsync(request.ProductId, request.ColorId);

        }
        public async Task<List<ProductColorResponse>> Handle(FilterProductColorQuery request, CancellationToken cancellationToken)
        {
            return await _productColorService.FilterProductColorAsync(request.Request);
        }
    }
}
