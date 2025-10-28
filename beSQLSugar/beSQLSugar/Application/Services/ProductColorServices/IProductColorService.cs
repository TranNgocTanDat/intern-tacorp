using beSQLSugar.Application.Dto.request.ProductColor;
using beSQLSugar.Application.Dto.response.ProductColor;
using beSQLSugar.Infrastructure.Database.Enities;

namespace beSQLSugar.Application.Services.ProductColorServices
{
    public interface IProductColorService
    {
        Task<ProductColorResponse> CreateAsync(ProductColorRequest request);
        Task<ProductColorResponse> UpdateAsync(int id, ProductColorRequest request);
        Task<bool> DeleteAsync(int id);
        Task<List<ProductColorResponse>> GetByProductIdAsync(int productId);
        Task<List<ProductColorResponse>> GetByProductIdAndColorIdAsync(int productId, int colorId);
        Task<List<ProductColorResponse>> GetAvailableColorsByProductIdAsync(int productId);
        Task<List<ProductColorResponse>> GetAllAsync();
        Task<List<ProductColorResponse>> FilterProductColorAsync(ProductColorFilterRequest request);
    }
}
