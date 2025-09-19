using beSQLSugar.Application.DTO.request;
using beSQLSugar.Application.DTO.response;
using beSQLSugar.Application.DTOs.request;
using beSQLSugar.Domain.Enities;
using beSQLSugar.Domain.Interfaces;

namespace beSQLSugar.Domain.RepositoryInterfaces
{
    // Tạo repository interface riêng cho AdminUser kế thùa IRepository và bổ sung các phương thức riêng
    public interface IAdminUserRepository : IRepository<AdminUser>
    {
        // Phương thức lấy người dùng qua username
        Task<AdminUser?> GetByUsernameAsync(string username);

        //Phương thức search người dùng qua AdminUserRequest
        Task<List<AdminUser>> SearchAsync(AdminUserSearchRequest request);
    }
}
