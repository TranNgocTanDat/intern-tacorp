using beSQLSugar.Application.Dto.response.HeroSection;
using beSQLSugar.Application.Features.HeroSection.Queries;
using beSQLSugar.Application.Services.HeroSectionServices;
using MediatR;

namespace beSQLSugar.Application.Features.HeroSection.Handlers
{
    public class HeroSectionQueryHandler :
           
            IRequestHandler<FilterHeroSectionQuery, List<HeroSectionResponse>>,
            IRequestHandler<GetAllWithDetailsQuery, List<HeroSectionResponse>>,
            IRequestHandler<GetHeroSectionWithDetailsQuery, HeroSectionResponse?>,
            IRequestHandler<GetHeroSectionWithPageHeroQuery, List<HeroSectionResponse>>

    {
        private readonly IHeroSectionService _heroSectionService;
        public HeroSectionQueryHandler(IHeroSectionService heroSectionService)
        {
            _heroSectionService = heroSectionService;
        }

        public async Task<List<HeroSectionResponse>> Handle(FilterHeroSectionQuery request, CancellationToken cancellationToken)
        {
            return await _heroSectionService.FilterAsync(request.FilterRequest!);
        }

        public async Task<HeroSectionResponse?> Handle(GetHeroSectionWithDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _heroSectionService.GetHeroSectionWithDetailsAsync(request.Id);
        }

        public async Task<List<HeroSectionResponse>> Handle(GetAllWithDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _heroSectionService.GetAllWithDetailsAsync();
        }

        public async Task<List<HeroSectionResponse>> Handle(GetHeroSectionWithPageHeroQuery request, CancellationToken cancellationToken)
        {
            return await _heroSectionService.GetHeroSectionsWithPageHeroAsync(request.PageHero);
        }
    }
}
