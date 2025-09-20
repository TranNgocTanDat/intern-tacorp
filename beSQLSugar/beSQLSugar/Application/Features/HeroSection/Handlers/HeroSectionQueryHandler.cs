using beSQLSugar.Application.DTO.response;
using beSQLSugar.Application.Features.HeroSection.Queries;
using beSQLSugar.Application.ServiceInterfaces;
using MediatR;

namespace beSQLSugar.Application.Features.HeroSection.Handlers
{
    public class HeroSectionQueryHandler :
        IRequestHandler<GetHeroSectionByIdQuery, HeroSectionResponse?>,
        IRequestHandler<GetAllHeroSectionQuery, List<HeroSectionResponse>>,
        IRequestHandler<FilterHeroSectionQuery, List<HeroSectionResponse>>

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

        public async Task<List<HeroSectionResponse>> Handle(FilterHeroSectionQuery request, CancellationToken cancellationToken)
        {
            return await _heroSectionService.FilterAsync(request.FilterRequest!);
        }
    }
}
