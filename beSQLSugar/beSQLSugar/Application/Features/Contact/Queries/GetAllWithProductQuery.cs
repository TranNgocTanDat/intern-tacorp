using beSQLSugar.Application.Dto.response.Contact;
using MediatR;

namespace beSQLSugar.Application.Features.Contact.Queries
{
    public class GetAllWithProductQuery : IRequest<List<ContactResponse>>
    {
        public GetAllWithProductQuery() { }
    }
}
