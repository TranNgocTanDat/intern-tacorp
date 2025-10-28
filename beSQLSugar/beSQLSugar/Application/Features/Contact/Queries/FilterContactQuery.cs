using beSQLSugar.Application.Dto.request.Contact;
using beSQLSugar.Application.Dto.response.Contact;
using MediatR;

namespace beSQLSugar.Application.Features.Contact.Queries
{
    public class FilterContactQuery : IRequest<List<ContactResponse>>
    {
        public ContactFilterRequest Request { get; set; }
        public FilterContactQuery(ContactFilterRequest request)
        {
            Request = request;
        }
    }
}
