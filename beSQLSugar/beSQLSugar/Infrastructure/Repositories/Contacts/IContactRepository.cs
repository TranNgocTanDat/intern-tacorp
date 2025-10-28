using beSQLSugar.Application.Dto.request.Contact;
using beSQLSugar.Infrastructure.Database.Enities;
using beSQLSugar.Infrastructure.Repositories;

namespace beSQLSugar.Infrastructure.Repositories.Contacts
{
    public interface IContactRepository : IRepository<Contact>
    {
        // Lọc liên hệ theo các tiêu chí trong request
        Task<List<Contact>> FilterContactAsync(ContactFilterRequest request);

        // Lấy tất cả liên hệ với product
        Task<List<Contact>> GetAllWithProductAsync();

        // Lấy liên hệ theo id với product
        Task<Contact?> GetByIdWithProductAsync(int id);

        // Update trạng thái đã xử lý
        Task<Contact> UpdateStatusAsync(int id, UpdateContactStatusRequest request);

        // Lấy liên hệ theo status
        Task<List<Contact>> GetByStatusAsync(string status);
    }
}
