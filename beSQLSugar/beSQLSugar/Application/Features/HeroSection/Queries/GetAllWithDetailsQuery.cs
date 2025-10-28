using beSQLSugar.Application.Dto.response.HeroSection;
using MediatR;

namespace beSQLSugar.Application.Features.HeroSection.Queries
{
    public class GetAllWithDetailsQuery : IRequest<List<HeroSectionResponse>>
    {
        public GetAllWithDetailsQuery() { }
    }
}
