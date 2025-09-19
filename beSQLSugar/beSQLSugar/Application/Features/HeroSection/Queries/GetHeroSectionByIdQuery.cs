using beSQLSugar.Application.DTO.response;
using MediatR;

namespace beSQLSugar.Application.Features.HeroSection.Queries
{
    public class GetHeroSectionByIdQuery : IRequest<HeroSectionResponse?>
    {
        public int Id { get; set; }
        public GetHeroSectionByIdQuery(int id)
        {
            Id = id;
        }
    }
}
