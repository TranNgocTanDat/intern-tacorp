using beSQLSugar.Application.DTO.response;
using MediatR;

namespace beSQLSugar.Application.Features.HeroSection.Queries
{
    public class GetPublishedHeroSectionQuery : IRequest<List<HeroSectionResponse>>
    {
        public GetPublishedHeroSectionQuery() { }
    }
}
