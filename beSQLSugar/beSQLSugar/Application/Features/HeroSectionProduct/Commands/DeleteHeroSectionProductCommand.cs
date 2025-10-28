using MediatR;

namespace beSQLSugar.Application.Features.HeroSectionProduct.Commands
{
    public class DeleteHeroSectionProductCommand : IRequest<bool>
    {
        public int HeroSectionProductId { get; set; }
        public DeleteHeroSectionProductCommand(int heroSectionProductId)
        {
            HeroSectionProductId = heroSectionProductId;
        }
    }
 
}
