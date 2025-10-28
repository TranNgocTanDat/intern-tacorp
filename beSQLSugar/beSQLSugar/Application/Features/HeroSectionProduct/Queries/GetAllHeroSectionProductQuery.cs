using beSQLSugar.Application.Dto.response.HeroSectionProduct;
using MediatR;

namespace beSQLSugar.Application.Features.HeroSectionProduct.Queries
{
    public class GetAllHeroSectionProductQuery : IRequest<List<HeroSectionProductResponse>>
    {
        public GetAllHeroSectionProductQuery() { }
    }
}
