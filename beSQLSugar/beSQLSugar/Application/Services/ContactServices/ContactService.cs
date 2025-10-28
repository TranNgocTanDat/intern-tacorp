using AutoMapper;
using beSQLSugar.Application.Dto.request.Contact;
using beSQLSugar.Application.Dto.response.Contact;
using beSQLSugar.Infrastructure.Database.Enities;
using beSQLSugar.Infrastructure.Repositories.Contacts;

namespace beSQLSugar.Application.Services.ContactServices
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;
        private readonly IMapper _mapper;
        public ContactService(IContactRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ContactResponse> AddContactAsync(ContactRequest request)
        {
            var entity = _mapper.Map<Contact>(request);
            var inserted = await _repository.AddAsync(entity);
            return _mapper.Map<ContactResponse>(inserted);

        }

        public async Task<bool> DeleteContactAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) throw new Exception("Contact not found");
            await _repository.DeleteAsync(id);
            return true;
        }

        public async Task<List<ContactResponse>> FilterContactAsync(ContactFilterRequest request)
        {
            var contacts = await _repository.FilterContactAsync(request);
            return _mapper.Map<List<ContactResponse>>(contacts);
        }

        public async Task<List<ContactResponse>> GetAllWithProductAsync()
        {
            var contacts = await _repository.GetAllWithProductAsync();
            return _mapper.Map<List<ContactResponse>>(contacts);
        }

        public async Task<ContactResponse?> GetByIdWithProductAsync(int id)
        {
            var contact = await _repository.GetByIdWithProductAsync(id);
            return _mapper.Map<ContactResponse?>(contact);
        }

        public async Task<List<ContactResponse>> GetByStatusAsync(string status)
        {
            var contacts = await _repository.GetByStatusAsync(status);
            return _mapper.Map<List<ContactResponse>>(contacts);

        }

        public async Task<ContactResponse> UpdateStatusAsync(int id, UpdateContactStatusRequest request)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) throw new Exception("Contact not found");
            var updated = await _repository.UpdateStatusAsync(id, request);
            return _mapper.Map<ContactResponse>(updated);
                
        }
    }
}
