using beSQLSugar.Application.DTO.response;
using beSQLSugar.Application.DTOs.request;
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
