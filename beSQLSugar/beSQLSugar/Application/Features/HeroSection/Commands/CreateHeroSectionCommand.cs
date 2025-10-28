using beSQLSugar.Application.Dto.request.HeroSection;
using beSQLSugar.Application.Dto.response.HeroSection;
using MediatR;
using System.Security.Claims;

namespace beSQLSugar.Application.Features.HeroSection.Commands
{
    public class CreateHeroSectionCommand : IRequest<HeroSectionResponse>
    {
        public HeroSectionRequest Request { get; set; }
        public ClaimsPrincipal User { get; set; }

        public CreateHeroSectionCommand(HeroSectionRequest request, ClaimsPrincipal user)
        {
            Request = request;
            User = user;
        }
    }
}
