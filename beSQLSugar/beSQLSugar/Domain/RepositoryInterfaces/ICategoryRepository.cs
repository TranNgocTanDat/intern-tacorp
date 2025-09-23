using beSQLSugar.Application.DTOs.request;
using beSQLSugar.Domain.Enities;
using beSQLSugar.Domain.Interfaces;

namespace beSQLSugar.Domain.RepositoryInterfaces
{
    // Tạo repository interface riêng cho Category kế thùa IRepository
    public interface ICategoryRepository : IRepository<Category>
    {
        // Category theo name
        public Task<Category?> GetByNameAsync(string name);

        // Search Category theo CategoryFilterRequest
        public Task<List<Category>> FilterAsync(CategoryFilterRequest request);
    }
}
