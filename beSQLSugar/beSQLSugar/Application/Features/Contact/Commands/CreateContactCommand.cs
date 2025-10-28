using beSQLSugar.Application.Dto.request.Contact;
using beSQLSugar.Application.Dto.response.Contact;
using MediatR;

namespace beSQLSugar.Application.Features.Contact.Commands
{
    public class CreateContactCommand : IRequest<ContactResponse>
    {
        public ContactRequest Request { get; set; }
        public CreateContactCommand(ContactRequest request)
        {
            Request = request;
        }
    }
    
}
