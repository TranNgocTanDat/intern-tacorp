using beSQLSugar.Application.Dto.request.ProductSpec;
using beSQLSugar.Infrastructure.Database.Enities;
using beSQLSugar.Infrastructure.Repositories;

namespace beSQLSugar.Infrastructure.Repositories.ProductSpecs
{
    public interface IProductSpecRepository : IRepository<ProductSpec>
    {
        // Lấy danh sách ProductSpec theo ProductId
        Task<List<ProductSpec>> GetByProductIdAsync(int productId);

        Task<List<ProductSpec>> GetAllWithProductAsync();

        Task<List<ProductSpec>> FilterProductSpecs(ProductSpecFilterRequest request);
    }
}
