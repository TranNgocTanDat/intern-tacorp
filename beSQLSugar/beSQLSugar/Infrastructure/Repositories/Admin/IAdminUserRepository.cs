using beSQLSugar.Application.Dto.request.Admin;

using beSQLSugar.Infrastructure.Database.Enities;

namespace beSQLSugar.Infrastructure.Repositories.AdminRepository
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
