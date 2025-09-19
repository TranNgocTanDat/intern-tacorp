using beSQLSugar.Application.DTO.request;
using beSQLSugar.Application.DTO.response;
using beSQLSugar.Application.DTOs.request;
using beSQLSugar.Domain.Enities;
using beSQLSugar.Domain.RepositoryInterfaces;
using beSQLSugar.Infrastructure.Database;

namespace beSQLSugar.Infrastructure.Repositories
{
    // Triển khai repository dành riêng cho AdminUser.
    // Kế thừa các phương thức CRUD chung từ BaseRepository và triển khai các phương thức từ IAdminUserRepository.
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

        public async Task<List<AdminUser>> SearchAsync(AdminUserSearchRequest request)
        {
            var query = _context.Db.Queryable<AdminUser>();
            if (!string.IsNullOrEmpty(request.Username))
            {
                query = query.Where(u => u.Username.Contains(request.Username));
            }
            if (!string.IsNullOrEmpty(request.Email))
            {
                query = query.Where(u => u.Email != null && u.Email.Contains(request.Email));
            }
            if (!string.IsNullOrEmpty(request.FullName))
            {
                query = query.Where(u => u.FullName != null && u.FullName.Contains(request.FullName));
            }
            if (!string.IsNullOrEmpty(request.Phone))
            {
                query = query.Where(u => u.Phone != null && u.Phone.Contains(request.Phone));
            }
            if (request.IsActive.HasValue)
                query = query.Where(u => u.IsActive == request.IsActive.Value);

            return await query.ToListAsync();
        }
    }
}
