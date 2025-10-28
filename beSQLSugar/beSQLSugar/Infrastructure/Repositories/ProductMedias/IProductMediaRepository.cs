using beSQLSugar.Application.Dto.request.ProductMedia;
using beSQLSugar.Infrastructure.Database.Enities;
using beSQLSugar.Infrastructure.Repositories;

namespace beSQLSugar.Infrastructure.Repositories.ProductMedias
{
    public interface IProductMediaRepository : IRepository<ProductMedia>
    {
        // Lấy danh sách ProductMedia theo ProductId
        Task<List<ProductMedia>> GetByProductIdAsync(int productId);
        Task<List<ProductMedia>> FilterProductMedia(ProductMediaFilterRequest request);
        Task<List<ProductMedia>> GetAllWithProductAsync();
    }
}
