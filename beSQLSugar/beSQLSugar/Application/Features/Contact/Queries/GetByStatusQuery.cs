using beSQLSugar.Application.Dto.response.Contact;
using MediatR;

namespace beSQLSugar.Application.Features.Contact.Queries
{
    public class GetByStatusQuery : IRequest<List<ContactResponse>>
    {
        public string Status { get; set; }
        public GetByStatusQuery(string status)
        {
            Status = status;
        }
    }
}
