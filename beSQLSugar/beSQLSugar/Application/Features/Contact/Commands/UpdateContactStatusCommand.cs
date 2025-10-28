using beSQLSugar.Application.Dto.request.Contact;
using beSQLSugar.Application.Dto.response.Contact;
using MediatR;

namespace beSQLSugar.Application.Features.Contact.Commands
{
    public class UpdateContactStatusCommand : IRequest<ContactResponse>
    {
        public int Id { get; set; }
        public UpdateContactStatusRequest? Request { get; set; }
        public UpdateContactStatusCommand(int id, UpdateContactStatusRequest? request)
        {
            Id = id;
            Request = request;
        }
    }
}
