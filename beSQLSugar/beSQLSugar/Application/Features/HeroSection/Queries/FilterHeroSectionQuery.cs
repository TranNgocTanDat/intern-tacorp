using beSQLSugar.Application.Dto.request.HeroSection;
using beSQLSugar.Application.Dto.response.HeroSection;
using MediatR;

namespace beSQLSugar.Application.Features.HeroSection.Queries
{
    public class FilterHeroSectionQuery : IRequest<List<HeroSectionResponse>>
    {
        public HeroSectionFilterRequest? FilterRequest { get; set; }
        public FilterHeroSectionQuery(HeroSectionFilterRequest? filterRequest)
        {
            FilterRequest = filterRequest;
        }
    }
}
