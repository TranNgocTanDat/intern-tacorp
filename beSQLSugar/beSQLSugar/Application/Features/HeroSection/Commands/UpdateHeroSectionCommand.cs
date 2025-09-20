using beSQLSugar.Application.DTO.request;
using beSQLSugar.Application.DTO.response;
using MediatR;

namespace beSQLSugar.Application.Features.HeroSection.Commands
{
    public class UpdateHeroSectionCommand : IRequest<HeroSectionResponse>
    {
        public int Id { get; set; }
        public HeroSectionRequest? Request { get; set; }
        public UpdateHeroSectionCommand(int id, HeroSectionRequest? request)
        {
            Id = id;
            Request = request;
        }
    }
}
