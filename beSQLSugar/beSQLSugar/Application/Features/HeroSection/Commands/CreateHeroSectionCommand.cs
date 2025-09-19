using beSQLSugar.Application.DTO.request;
using beSQLSugar.Application.DTO.response;
using MediatR;

namespace beSQLSugar.Application.Features.HeroSection.Commands
{
    public class CreateHeroSectionCommand : IRequest<HeroSectionResponse>
    {
        public HeroSectionRequest Request { get; set; }
        public CreateHeroSectionCommand(HeroSectionRequest request)
        {
            Request = request;
        }
    }
}
