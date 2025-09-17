using beSQLSugar.Domain.Enities;
using beSQLSugar.Domain.Interfaces;

namespace beSQLSugar.Domain.RepositoryInterfaces
{
    public interface IAdminUserRepository : IRepository<AdminUser>
    {
        Task<AdminUser?> GetByUsernameAsync(string username);
    }
}
