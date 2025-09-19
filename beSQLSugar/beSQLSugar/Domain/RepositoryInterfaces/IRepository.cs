namespace beSQLSugar.Domain.Interfaces
{
    // Tạo repository interface chung cho các entity với các phương thức CRUD cơ bản
    public interface IRepository<T> where T : class, new()
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T?> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(int id);
    }
}
