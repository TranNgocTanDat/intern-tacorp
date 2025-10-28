using beSQLSugar.Application.Dto.request.ProductSpec;
using beSQLSugar.Application.Dto.request.ProductStorage;
using beSQLSugar.Infrastructure.Database.Enities;

namespace beSQLSugar.Infrastructure.Repositories.ProductStorages
{
    public interface IProductStorageRepository : IRepository<ProductStorage>
    {
        Task<List<ProductStorage>> GetByProductIdAsync(int productId);

        Task<List<ProductStorage>> GetAllWithProductAsync();

        Task<List<ProductStorage>> FilterProductStorageAsync(ProductStorageFilterRequest request);
    }
}
