using beSQLSugar.Application.Dto.response.HeroSection;
using MediatR;

namespace beSQLSugar.Application.Features.HeroSection.Queries
{
    public class GetHeroSectionWithPageHeroQuery : IRequest<List<HeroSectionResponse>>
    {
        public string PageHero { get; set; }
        public GetHeroSectionWithPageHeroQuery(string pageHero)
        {
            PageHero = pageHero;
        }
    }
}
