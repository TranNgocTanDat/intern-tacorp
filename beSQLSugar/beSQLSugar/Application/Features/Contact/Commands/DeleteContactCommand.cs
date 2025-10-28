using MediatR;

namespace beSQLSugar.Application.Features.Contact.Commands
{
    public class DeleteContactCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public DeleteContactCommand(int id)
        {
            Id = id;
        }
    }
}
