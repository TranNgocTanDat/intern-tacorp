using beSQLSugar.Application.DTO.request;
using beSQLSugar.Application.DTO.response;

namespace beSQLSugar.Application.ServiceInterfaces
{
    public interface IAdminUserService
    {
        Task<AdminUserResponse> CreateAsync(AdminUserRequest request);
        Task<AdminUserResponse?> GetByIdAsync(int id);
        Task<List<AdminUserResponse>> GetAllAsync();
        Task<AdminUserResponse?> UpdateAsync(int id, AdminUserRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
