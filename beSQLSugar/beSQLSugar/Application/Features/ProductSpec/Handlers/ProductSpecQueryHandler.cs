using beSQLSugar.Application.Dto.response.ProductSpec;
using beSQLSugar.Application.Features.ProductSpec.Queries;
using beSQLSugar.Application.Services.ProductSpecServivces;
using MediatR;

namespace beSQLSugar.Application.Features.ProductSpec.Handlers
{
    public class ProductSpecQueryHandler : 
        IRequestHandler<GetSpecProductByIdQuery, List<ProductSpecResponse>>,
        IRequestHandler<FilterSpecQuery, List<ProductSpecResponse>>,
        IRequestHandler<GetAllSpecQuery, List<ProductSpecResponse>>
    {
        private readonly IProductSpecService _productSpecService;
        public ProductSpecQueryHandler(IProductSpecService productSpecService)
        {
            _productSpecService = productSpecService;
        }
        public async Task<List<ProductSpecResponse>> Handle(GetSpecProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _productSpecService.GetSpecsByProductIdAsync(request.ProductId);
        }

        public async Task<List<ProductSpecResponse>> Handle(FilterSpecQuery request, CancellationToken cancellationToken)
        {
            return await _productSpecService.FilterProductSpec(request.FilterRequest!);
        }

        public async Task<List<ProductSpecResponse>> Handle(GetAllSpecQuery request, CancellationToken cancellationToken)
        {
            return await _productSpecService.GetAllSpecAsync();
        }
    }
}
