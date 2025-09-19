using beSQLSugar.Application.DTO.response;
using MediatR;

namespace beSQLSugar.Application.Features.HeroSection.Queries
{
    public class GetHeroSectionByPageHeroQuery : IRequest<List<HeroSectionResponse>>
    {
        public string PageHero { get; set; }
        public GetHeroSectionByPageHeroQuery(string pageHero)
        {
            PageHero = pageHero;
        }
    }
 
}
