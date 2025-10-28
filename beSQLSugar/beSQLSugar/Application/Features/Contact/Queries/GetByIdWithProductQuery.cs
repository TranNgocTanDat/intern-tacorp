using beSQLSugar.Application.Dto.response.Contact;
using MediatR;

namespace beSQLSugar.Application.Features.Contact.Queries
{
    public class GetByIdWithProductQuery : IRequest<ContactResponse>
    {
        public int Id { get; set; }
        public GetByIdWithProductQuery(int id)
        {
            Id = id;
        }
    }
}
