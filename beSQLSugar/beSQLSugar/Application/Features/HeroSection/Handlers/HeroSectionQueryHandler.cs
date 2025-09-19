using beSQLSugar.Application.DTO.response;
using beSQLSugar.Application.Features.HeroSection.Queries;
using beSQLSugar.Application.ServiceInterfaces;
using MediatR;

namespace beSQLSugar.Application.Features.HeroSection.Handlers
{
    public class HeroSectionQueryHandler :
        IRequestHandler<GetHeroSectionByIdQuery, HeroSectionResponse?>,
        IRequestHandler<GetAllHeroSectionQuery, List<HeroSectionResponse>>,
        IRequestHandler<SearchHeroSectionQuery, List<HeroSectionResponse>>,
        IRequestHandler<GetHeroSectionByDateRangeQuery, List<HeroSectionResponse>>,
        IRequestHandler<GetPublishedHeroSectionQuery, List<HeroSectionResponse>>,
        IRequestHandler<GetHeroSectionByPageHeroQuery, List<HeroSectionResponse>>,
        IRequestHandler<GetHeroSectionWithProductsQuery, HeroSectionResponse?>

    {
        private readonly IHeroSectionService _heroSectionService;
        public HeroSectionQueryHandler(IHeroSectionService heroSectionService)
        {
            _heroSectionService = heroSectionService;
        }

        public async Task<HeroSectionResponse?> Handle(GetHeroSectionByIdQuery request, CancellationToken cancellationToken)
        {
            return await _heroSectionService.GetByIdAsync(request.Id);
        }

        public async Task<List<HeroSectionResponse>> Handle(GetAllHeroSectionQuery request, CancellationToken cancellationToken)
        {
            return await _heroSectionService.GetAllAsync();
        }

        public async Task<List<HeroSectionResponse>> Handle(SearchHeroSectionQuery request, CancellationToken cancellationToken)
        {
            return await _heroSectionService.SearchAsync(request.Keyword);
        }

        public async Task<List<HeroSectionResponse>> Handle(GetHeroSectionByDateRangeQuery request, CancellationToken cancellationToken)
        {
            return await _heroSectionService.GetByDateRangeAsync(request.From, request.To);
        }

        public async Task<List<HeroSectionResponse>> Handle(GetPublishedHeroSectionQuery request, CancellationToken cancellationToken)
        {
            return await _heroSectionService.GetPublishedAsync();
        }

        public async Task<List<HeroSectionResponse>> Handle(GetHeroSectionByPageHeroQuery request, CancellationToken cancellationToken)
        {
            return await _heroSectionService.GetByPageHeroAsync(request.PageHero);
        }

        public async Task<HeroSectionResponse?> Handle(GetHeroSectionWithProductsQuery request, CancellationToken cancellationToken)
        {
            return await _heroSectionService.GetWithProductsAsync(request.Id);
        }
    }
}
