using beSQLSugar.Application.Dto.request.Contact;
using beSQLSugar.Application.Dto.response.Contact;

namespace beSQLSugar.Application.Services.ContactServices
{
    public interface IContactService
    {
        // Lọc liên hệ theo các tiêu chí trong request
        Task<List<ContactResponse>> FilterContactAsync(ContactFilterRequest request);
        // Lấy tất cả liên hệ với product
        Task<List<ContactResponse>> GetAllWithProductAsync();
        // Lấy liên hệ theo id với product
        Task<ContactResponse?> GetByIdWithProductAsync(int id);
        // Thêm liên hệ mới
        Task<ContactResponse> AddContactAsync(ContactRequest request);
        // Update trạng thái đã xử lý
        Task<ContactResponse> UpdateStatusAsync(int id, UpdateContactStatusRequest request);
        // Lấy liên hệ theo status
        Task<List<ContactResponse>> GetByStatusAsync(string status);
        // Xóa liên hệ
        Task<bool> DeleteContactAsync(int id);


    }
}
