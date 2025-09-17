using beSQLSugar.Domain.Enities;
using beSQLSugar.Domain.RepositoryInterfaces;
using beSQLSugar.Infrastructure.Database;

namespace beSQLSugar.Infrastructure.Repositories
{
    public class AdminUserRepository : BaseRepository<AdminUser>, IAdminUserRepository
    {
        public AdminUserRepository(SqlSugarDbContext context) : base(context)
        {
        }
        public async Task<AdminUser?> GetByUsernameAsync(string username)
        {
            return await _context.Db.Queryable<AdminUser>()
                .Where(u => u.Username == username)
                .FirstAsync();
        }
    }
}
