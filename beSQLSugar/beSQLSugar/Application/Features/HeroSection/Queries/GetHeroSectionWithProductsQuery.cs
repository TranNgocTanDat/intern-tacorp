using beSQLSugar.Application.DTO.response;
using MediatR;

namespace beSQLSugar.Application.Features.HeroSection.Queries
{
    public class GetHeroSectionWithProductsQuery : IRequest<HeroSectionResponse?>
    {
        public int Id { get; set; }
        public GetHeroSectionWithProductsQuery(int id)
        {
            Id = id;
        }
    }
}
