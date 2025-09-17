
using beSQLSugar.Data;
using SqlSugar;

namespace beSQLSugar.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class, new()
    {
        private readonly SqlSugarClient _db;

        public BaseRepository(DbContext context)
        {
            _db = context.Db;
        }
        public async Task<T> AddAsync(T entity)
        {
            var id = await _db.Insertable(entity).ExecuteReturnIdentityAsync();
            var insertedEntity = await _db.Queryable<T>().InSingleAsync(id);
                return insertedEntity!;
            
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _db.Deleteable<T>().In(id).ExecuteCommandAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {   
            return await _db.Queryable<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _db.Queryable<T>().InSingleAsync(id);
        }

        public Task<int> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
