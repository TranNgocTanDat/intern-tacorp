using beSQLSugar.Application.Dto.response.HeroSectionProduct;
using beSQLSugar.Application.Features.HeroSectionProduct.Queries;
using beSQLSugar.Application.Services.HeroSectionProductServices;
using MediatR;

namespace beSQLSugar.Application.Features.HeroSectionProduct.Handlers
{
    public class HeroSectionProductQueryHandler :
            IRequestHandler<GetByHeroSectionIdQuery, List<HeroSectionProductResponse>>,
            IRequestHandler<GetByHeroSectionAndProductQuery, HeroSectionProductResponse>,
            IRequestHandler<GetAllHeroSectionProductQuery, List<HeroSectionProductResponse>>,
            IRequestHandler<FilterHRPQuery, List<HeroSectionProductResponse>>
    {
        private readonly IHeroSectionProductService _service;
        public HeroSectionProductQueryHandler(IHeroSectionProductService service)
        {
            _service = service;
        }

        public async Task<List<HeroSectionProductResponse>> Handle(GetByHeroSectionIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetByHeroSectionIdAsync(request.HeroSectionId);
        }

        public async Task<HeroSectionProductResponse> Handle(GetByHeroSectionAndProductQuery request, CancellationToken cancellationToken)
        {
            var result = await _service.GetByHeroSectionAndProductAsync(request.HeroSectionId, request.ProductId);
            return result!;
        }

        public async Task<List<HeroSectionProductResponse>> Handle(GetAllHeroSectionProductQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAllHRPAsync();
        }

        // Lọc HeroSectionProduct
        public async Task<List<HeroSectionProductResponse>> Handle(FilterHRPQuery request, CancellationToken cancellationToken)
        {
            return await _service.FilterAsync(request.FilterRequest!);
        }

    }
}
