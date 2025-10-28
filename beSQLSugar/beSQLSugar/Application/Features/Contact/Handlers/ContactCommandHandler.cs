using beSQLSugar.Application.Dto.response.Contact;
using beSQLSugar.Application.Features.Contact.Commands;
using beSQLSugar.Application.Services.ContactServices;
using MediatR;

namespace beSQLSugar.Application.Features.Contact.Handlers
{
    public class ContactCommandHandler : 
        IRequestHandler<CreateContactCommand, ContactResponse>,
        IRequestHandler<UpdateContactStatusCommand, ContactResponse>,
        IRequestHandler<DeleteContactCommand, bool>
    {
        private readonly IContactService _contactService;
        public ContactCommandHandler(IContactService contactService)
        {
            _contactService = contactService;
        }
        public async Task<ContactResponse> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var result = await _contactService.AddContactAsync(request.Request);
            if (result is null)
                throw new InvalidOperationException("Failed to create Contact.");
            return result;
        }
        
        public async Task<ContactResponse> Handle(UpdateContactStatusCommand request, CancellationToken cancellationToken)
        {
            if (request.Request == null)
                throw new ArgumentNullException(nameof(request.Request), "UpdateContactStatusRequest cannot be null.");
            return await _contactService.UpdateStatusAsync(request.Id, request.Request);
        }

        public async Task<bool> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            var success = await _contactService.DeleteContactAsync(request.Id);
            if (!success)
                throw new InvalidOperationException($"Failed to delete Contact with ID {request.Id}.");
            return success;
        }
    }
}
