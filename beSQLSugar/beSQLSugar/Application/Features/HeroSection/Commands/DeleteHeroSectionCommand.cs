using MediatR;

namespace beSQLSugar.Application.Features.HeroSection.Commands
{
    public class DeleteHeroSectionCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public DeleteHeroSectionCommand(int id)
        {
            Id = id;
        }
    }
}
