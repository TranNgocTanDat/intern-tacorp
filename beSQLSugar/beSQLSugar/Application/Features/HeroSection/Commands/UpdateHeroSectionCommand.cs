using beSQLSugar.Application.Dto.request.HeroSection;
using beSQLSugar.Application.Dto.response.HeroSection;
using MediatR;
using System.Security.Claims;

namespace beSQLSugar.Application.Features.HeroSection.Commands
{
    public class UpdateHeroSectionCommand : IRequest<HeroSectionResponse>
    {
        public int Id { get; set; }
        public HeroSectionRequest? Request { get; set; }
        public ClaimsPrincipal User { get; set; }

        public UpdateHeroSectionCommand(int id, HeroSectionRequest? request, ClaimsPrincipal user)
        {
            Id = id;
            Request = request;
            User = user;
        }
    }
}
