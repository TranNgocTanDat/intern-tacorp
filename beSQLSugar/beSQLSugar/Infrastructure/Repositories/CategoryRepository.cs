using beSQLSugar.Application.DTOs.request;
using beSQLSugar.Domain.Enities;
using beSQLSugar.Domain.RepositoryInterfaces;
using beSQLSugar.Infrastructure.Database;

namespace beSQLSugar.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(SqlSugarDbContext context) : base(context)
        {
        }

        public async Task<List<Category>> FilterAsync(CategoryFilterRequest request)
        {
            var query = _context.Db.Queryable<Category>();
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(c => c.Name != null && c.Name.Contains(request.Name));
            }
            if (!string.IsNullOrEmpty(request.Description))
            {
                query = query.Where(c => c.Description != null && c.Description.Contains(request.Description));
            }
            if (request.IsActive.HasValue)
            {
                query = query.Where(c => c.IsActive == request.IsActive.Value);
            }
            return await query.ToListAsync();
        }

        public async Task<Category?> GetByNameAsync(string name)
        {
            var category = _context.Db.Queryable<Category>()
                .Where(c => c.Name == name)
                .FirstAsync();
            return await category;
        }
    }
}
