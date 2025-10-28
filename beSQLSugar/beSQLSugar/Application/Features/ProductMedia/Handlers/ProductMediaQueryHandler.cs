using beSQLSugar.Application.Dto.response.ProductMedia;
using beSQLSugar.Application.Features.ProductMedia.Queries;
using beSQLSugar.Application.Services.ProductMediaServices;
using MediatR;

namespace beSQLSugar.Application.Features.ProductMedia.Handlers
{
    public class ProductMediaQueryHandler : 
        IRequestHandler<GetMediaProductByIdQuery, List<ProductMediaResponse>>,
        IRequestHandler<FilterProductMediaQuery, List<ProductMediaResponse>>,
        IRequestHandler<GetAllMediaProductQuery, List<ProductMediaResponse>>
    {
        private readonly IProductMediaService _productMediaService;
        public ProductMediaQueryHandler(IProductMediaService productMediaService)
        {
            _productMediaService = productMediaService;
        }

        public async Task<List<ProductMediaResponse>> Handle(GetAllMediaProductQuery request, CancellationToken cancellationToken)
        {
            return await _productMediaService.GetAllMediaAsync();
        }

        public async Task<List<ProductMediaResponse>> Handle(GetMediaProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _productMediaService.GetMediaByProductIdAsync(request.ProductId);
        }

        public async Task<List<ProductMediaResponse>> Handle(FilterProductMediaQuery request, CancellationToken cancellationToken)
        {
            return await _productMediaService.FilterProductMedia(request.Request!);
        }
    }
}
