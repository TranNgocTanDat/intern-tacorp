using beSQLSugar.Application.Dto.request.ProductColor;
using beSQLSugar.Infrastructure.Database.Enities;

namespace beSQLSugar.Infrastructure.Repositories.ProductColors
{
    public interface IProductColorRepository : IRepository<ProductColor>
    {
        Task<List<ProductColor>> GetAllWithProductAsync();
        /// Lấy danh sách màu theo ProductId.
        Task<List<ProductColor>> GetByProductIdAsync(int productId);

        Task<List<ProductColor>> GetByProductIdAndColorIdAsync(int productId, int colorId);

        /// Lấy danh sách màu có sẵn (IsAvailable = true) theo ProductId.
        Task<List<ProductColor>> GetAvailableColorsByProductIdAsync(int productId);

        Task<List<ProductColor>> FilterProductColorAsync(ProductColorFilterRequest request);
    }
}
