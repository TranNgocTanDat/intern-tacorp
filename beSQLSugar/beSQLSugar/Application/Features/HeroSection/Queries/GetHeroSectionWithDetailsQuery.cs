using beSQLSugar.Application.Dto.response.HeroSection;
using MediatR;

namespace beSQLSugar.Application.Features.HeroSection.Queries
{
    public class GetHeroSectionWithDetailsQuery : IRequest<HeroSectionResponse>
    {
        public int Id { get; set; }
        public GetHeroSectionWithDetailsQuery(int id)
        {
            Id = id;
        }
    }
}
