using beSQLSugar.Application.DTO.response;
using MediatR;

namespace beSQLSugar.Application.Features.HeroSection.Queries
{
    public class GetAllHeroSectionQuery : IRequest<List<HeroSectionResponse>>
    {
        public GetAllHeroSectionQuery() { }
    }
}
