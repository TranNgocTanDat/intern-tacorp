using beSQLSugar.Application.Dto.response.HeroSectionProduct;
using MediatR;

namespace beSQLSugar.Application.Features.HeroSectionProduct.Queries
{
    public class GetByHeroSectionIdQuery : IRequest<List<HeroSectionProductResponse>>
    {
        public int HeroSectionId { get; set; }
        public GetByHeroSectionIdQuery(int heroSectionId)
        {
            HeroSectionId = heroSectionId;
        }
    }
}
