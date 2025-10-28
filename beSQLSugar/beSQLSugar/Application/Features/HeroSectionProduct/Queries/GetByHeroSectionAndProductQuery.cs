using beSQLSugar.Application.Dto.response.HeroSectionProduct;
using MediatR;

namespace beSQLSugar.Application.Features.HeroSectionProduct.Queries
{
    public class GetByHeroSectionAndProductQuery : IRequest<HeroSectionProductResponse>
    {
        public int HeroSectionId { get; set; }
        public int ProductId { get; set; }
        public GetByHeroSectionAndProductQuery(int heroSectionId, int productId)
        {
            HeroSectionId = heroSectionId;
            ProductId = productId;
        }
    }
}
