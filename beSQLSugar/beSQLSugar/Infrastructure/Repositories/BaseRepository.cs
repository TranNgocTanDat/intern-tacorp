using beSQLSugar.Domain.Interfaces;
using beSQLSugar.Infrastructure.Database;
using SqlSugar;

namespace beSQLSugar.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class, new()
    {
        protected readonly SqlSugarDbContext _context;

        public BaseRepository(SqlSugarDbContext context)
        {
            _context = context;
        }
        public async Task<T> AddAsync(T entity)
        {
            var id = await _context.Db.Insertable(entity).ExecuteReturnIdentityAsync();
            var insertedEntity = await _context.Db.Queryable<T>().InSingleAsync(id);
                return insertedEntity!;
            
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _context.Db.Deleteable<T>().In(id).ExecuteCommandAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {   
            return await _context.Db.Queryable<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Db.Queryable<T>().InSingleAsync(id);
        }

        public async Task<int> UpdateAsync(T entity)
        {
            return await _context.Db.Updateable(entity).ExecuteCommandAsync();
        }
    }
}
