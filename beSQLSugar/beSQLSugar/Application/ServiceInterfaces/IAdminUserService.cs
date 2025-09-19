using beSQLSugar.Application.DTO.request;
using beSQLSugar.Application.DTO.response;
using beSQLSugar.Application.DTOs.request;

namespace beSQLSugar.Application.ServiceInterfaces
{
    // định nghĩa interface cho dịch vụ quản lý AdminUser
    // bao gồm các phương thức CRUD và trả về các đối tượng DTO
    public interface IAdminUserService
    {
        // Tạo mới người dùng qua DTO request và trả về DTO response
        Task<AdminUserResponse> CreateAsync(AdminUserRequest request);

        // Lấy người dùng theo id và trả về DTO response hoặc null nếu không tìm thấy
        Task<AdminUserResponse?> GetByIdAsync(int id);

        // Lấy tất cả người dùng và trả về danh sách DTO response
        Task<List<AdminUserResponse>> GetAllAsync();

        // Cập nhật người dùng theo id và DTO request, trả về DTO response hoặc null nếu không tìm thấy
        Task<AdminUserResponse?> UpdateAsync(int id, AdminUserRequest request);

        // Xóa người dùng theo id, trả về true nếu xóa thành công, false nếu không tìm thấy
        Task<bool> DeleteAsync(int id);

        // Search AdminUsers by AdminUserSearchRequest and return AdminUserResponse
        Task<List<AdminUserResponse>> SearchAsync(AdminUserSearchRequest request);

    }
}
