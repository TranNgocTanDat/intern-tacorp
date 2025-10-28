using beSQLSugar.Application.Dto.request.Product;
using beSQLSugar.Application.Dto.response.Product;
using System.Security.Claims;

namespace beSQLSugar.Application.Services.ProductServices
{
    public interface IProductService
    {
        // CRUD cơ bản
        Task<ProductResponse?> AddProductAsync(ProductRequest product);
        Task<bool> DeleteProductAsync(int id);
        Task<List<ProductResponse>> GetAllProductsAsync();
        Task<ProductResponse?> GetProductByIdAsync(int id);
        Task<ProductResponse> UpdateProductAsync(int id, ProductRequest product);

        // Lấy product theo name
        Task<ProductResponse?> GetByNameAsync(string name);
        
        // Lấy Product theo slug
        Task<ProductResponse?> GetBySlugAsync(string slug);
        // Filter product dựa trên các tiêu chí trong ProductFilterRequest
        Task<List<ProductResponse>> FilterProductsAsync(ProductFilterRequest filterRequest);

        Task<ProductResponse?> GetProductWithDetailsAsync(int id);
        Task<List<ProductResponse>> GetFeaturedProductsAsync();
    }
}
