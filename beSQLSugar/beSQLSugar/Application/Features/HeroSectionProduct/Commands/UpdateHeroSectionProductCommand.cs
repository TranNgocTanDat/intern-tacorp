using beSQLSugar.Application.Dto.request.HeroSectionProduct;
using beSQLSugar.Application.Dto.response.HeroSectionProduct;
using MediatR;
using System.Security.Claims;

namespace beSQLSugar.Application.Features.HeroSectionProduct.Commands
{
    public class UpdateHeroSectionProductCommand : IRequest<HeroSectionProductResponse>
    {
        public int Id { get; set; }
        public HeroSectionProductRequest? Request { get; set; }
        public ClaimsPrincipal User { get; set; }
        public UpdateHeroSectionProductCommand(int id, HeroSectionProductRequest? request, ClaimsPrincipal user
            )
        {
           Id = id;
            Request = request;
            User = user;
        }
    }
}
