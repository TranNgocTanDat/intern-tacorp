
using beSQLSugar.Application.Dto.request.Category;
using beSQLSugar.Infrastructure.Database.Enities;

namespace beSQLSugar.Infrastructure.Repository.CategoryRepository
{
    // Tạo repository interface riêng cho Category kế thùa IRepository
    public interface ICategoryRepository : IRepository<Category>
    {
        // Category theo name
        Task<Category?> GetByNameAsync(string name);

        // Search Category theo CategoryFilterRequest
        Task<List<Category>> FilterAsync(CategoryFilterRequest request);

        //Lấy category theo Id kèm con + sản phẩm
        Task<Category?> GetCategoryWithDetailsAsync(int id);

        // Lấy toàn bộ category kèm con + sản phẩm
        Task<List<Category>> GetAllWithDetailsAsync();

        Task<List<Category>> GetCategoryChildrenAsync();

        Task<List<Category>> GetCategoriesAsync();
        
    }
}
