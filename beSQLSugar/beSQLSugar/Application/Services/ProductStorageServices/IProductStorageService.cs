using beSQLSugar.Application.Dto.request.ProductStorage;
using beSQLSugar.Application.Dto.response.ProductStorage;

namespace beSQLSugar.Application.Services.ProductStorageServices
{
    public interface IProductStorageService
    {
        Task<ProductStorageResponse> CreateAsync(ProductStorageRequest request);
        Task<ProductStorageResponse> UpdateAsync(int id, ProductStorageRequest request);
        Task<bool> DeleteAsync(int id);
        Task<List<ProductStorageResponse>> GetByIdAsync(int id);
        Task<List<ProductStorageResponse>> GetAllWithProductAsync();
        Task<List<ProductStorageResponse>> FilterProductStorageAsync(ProductStorageFilterRequest request);
    }
}
