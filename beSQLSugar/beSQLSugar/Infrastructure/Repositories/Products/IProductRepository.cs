using beSQLSugar.Application.Dto.request.Product;
using beSQLSugar.Infrastructure.Database.Enities;

namespace beSQLSugar.Infrastructure.Repositories.Products
{
    public interface IProductRepository : IRepository<Product>
    {
        // Lấy product theo name
        Task<Product?> GetByNameAsync(string name);
        
        // Lấy Product theo slug
        Task<Product?> GetBySlugAsync(string slug);

        // Filter product dựa trên các tiêu chí trong ProductFilterRequest
        Task<List<Product>> FilterProductsAsync(ProductFilterRequest filterRequest);

        Task<Product?> GetProductWithDetailsAsync(int id);
        Task<List<Product>> GetFeaturedProductsAsync();
    }
}
