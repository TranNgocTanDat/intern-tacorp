using MediatR;

namespace beSQLSugar.Application.Features.Partner.Commands
{
    public class DeletePartnerCommand : IRequest<bool>
    {
        public int Id { get; }
        public DeletePartnerCommand(int id)
        {
            Id = id;
        }
    }
}
