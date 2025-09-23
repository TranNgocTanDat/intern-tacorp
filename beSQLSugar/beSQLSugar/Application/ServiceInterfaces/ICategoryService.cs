using beSQLSugar.Application.DTO.request;
using beSQLSugar.Application.DTO.response;
using beSQLSugar.Application.DTOs.request;

namespace beSQLSugar.Application.ServiceInterfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllAsync();
        Task<CategoryResponse?> GetByIdAsync(int id);
        Task<CategoryResponse?> AddAsync(CategoryRequest request);
        Task<CategoryResponse?> UpdateAsync(int id, CategoryRequest request);
        Task<Boolean> DeleteAsync(int id);
        Task<List<CategoryResponse>> FilterAsync(CategoryFilterRequest request);
    }
}
