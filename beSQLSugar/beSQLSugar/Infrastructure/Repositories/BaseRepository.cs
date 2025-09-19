using beSQLSugar.Domain.Interfaces;
using beSQLSugar.Infrastructure.Database;
using SqlSugar;

namespace beSQLSugar.Infrastructure.Repositories
{
    // Triển khai IRepository với SqlSugar
    // Các repository cụ thể sẽ kế thùa BaseRepository để sử dụng các phương thức CRUD chung theo kiểu thực thẻ T
    public class BaseRepository<T> : IRepository<T> where T : class, new()
    {
        // Inject SqlSugarDbContext để truy cập Db
        protected readonly SqlSugarDbContext _context;

        // Khởi tạo với SqlSugarDbContext
        public BaseRepository(SqlSugarDbContext context)
        {
            _context = context;
        }

        // Thêm mới entity và trả về entity vừa được thêm
        public async Task<T?> AddAsync(T entity)
        {
            var id = await _context.Db.Insertable(entity).ExecuteReturnIdentityAsync();
            var insertedEntity = await _context.Db.Queryable<T>().InSingleAsync(id);
                return insertedEntity!;
            
        }

        // Xóa entity theo id và trả về số bản ghi bị xóa
        public async Task<int> DeleteAsync(int id)
        {
            return await _context.Db.Deleteable<T>().In(id).ExecuteCommandAsync();
        }

        // Lấy toàn bộ danh sách bản ghi của thực thể T
        public async Task<List<T>> GetAllAsync()
        {   
            return await _context.Db.Queryable<T>().ToListAsync();
        }

        // Lấy entity theo id
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Db.Queryable<T>().InSingleAsync(id);
        }

        // Cập nhật entity
        public async Task<int> UpdateAsync(T entity)
        {
            return await _context.Db.Updateable(entity).ExecuteCommandAsync();
        }
    }
}
