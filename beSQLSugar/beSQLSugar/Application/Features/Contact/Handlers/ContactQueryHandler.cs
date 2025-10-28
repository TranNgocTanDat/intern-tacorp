using beSQLSugar.Application.Dto.response.Contact;
using beSQLSugar.Application.Features.Contact.Queries;
using beSQLSugar.Application.Services.ContactServices;
using MediatR;

namespace beSQLSugar.Application.Features.Contact.Handlers
{
    public class ContactQueryHandler :
        IRequestHandler<GetAllWithProductQuery, List<ContactResponse>>,
        IRequestHandler<GetByIdWithProductQuery, ContactResponse?>,
        IRequestHandler<GetByStatusQuery, List<ContactResponse>>,
        IRequestHandler<FilterContactQuery, List<ContactResponse>>
    {
        private readonly IContactService _contactService;
        public ContactQueryHandler(IContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task<List<ContactResponse>> Handle(GetAllWithProductQuery request, CancellationToken cancellationToken)
        {
            var result = await _contactService.GetAllWithProductAsync();
            return result;
        }

        public async Task<ContactResponse?> Handle(GetByIdWithProductQuery request, CancellationToken cancellationToken)
        {
            var result = await _contactService.GetByIdWithProductAsync(request.Id);
            return result;
        }

        public async Task<List<ContactResponse>> Handle(GetByStatusQuery request, CancellationToken cancellationToken)
        {
            var result = await _contactService.GetByStatusAsync(request.Status);
            return result;
        }

        public async Task<List<ContactResponse>> Handle(FilterContactQuery request, CancellationToken cancellationToken)
        {
            var result = await _contactService.FilterContactAsync(request.Request);
            return result;
        }
    }
}
