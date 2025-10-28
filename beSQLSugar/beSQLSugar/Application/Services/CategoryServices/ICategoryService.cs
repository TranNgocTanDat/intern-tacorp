using beSQLSugar.Application.Dto.request.Category;
using beSQLSugar.Application.Dto.response.Category;
using System.Security.Claims;

namespace beSQLSugar.Application.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllAsync();
        Task<CategoryResponse?> GetByIdAsync(int id);
        Task<CategoryResponse?> AddAsync(CategoryRequest request, ClaimsPrincipal admin);
        Task<CategoryResponse?> UpdateAsync(int id, CategoryRequest request, ClaimsPrincipal admin);
        Task<bool> DeleteAsync(int id);
        Task<List<CategoryResponse>> FilterAsync(CategoryFilterRequest request);

        Task<CategoryResponse?> GetCategoryWithDetailsAsync(int id);
        Task<List<CategoryResponse>> GetAllWithDetailsAsync();
        Task<List<CategoryResponse>> GetCategoryChildrenAsync();
    }
}
