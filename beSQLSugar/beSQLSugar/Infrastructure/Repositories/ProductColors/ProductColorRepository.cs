using beSQLSugar.Application.Dto.request.ProductColor;
using beSQLSugar.Infrastructure.Database;
using beSQLSugar.Infrastructure.Database.Enities;
using SqlSugar;

namespace beSQLSugar.Infrastructure.Repositories.ProductColors
{
    public class ProductColorRepository : BaseRepository<ProductColor>, IProductColorRepository
    {
        public ProductColorRepository(SqlSugarDbContext context) : base(context)
        {
        }

        public async Task<List<ProductColor>> FilterProductColorAsync(ProductColorFilterRequest request)
        {
            var query = _context.Db.Queryable<ProductColor>()
                                   .Includes(pc => pc.Product) // load Product
                                   .Includes(pc => pc.MediaList); // load MediaList

            query = query.WhereIF(request.ProductId > 0, pc => pc.ProductId == request.ProductId)
                         .WhereIF(!string.IsNullOrEmpty(request.ColorName),
                                  pc => SqlFunc.Contains(pc.ColorName, request.ColorName))
                         .WhereIF(!string.IsNullOrEmpty(request.ColorCode),
                                  pc => pc.ColorCode == request.ColorCode)
                         .WhereIF(!string.IsNullOrEmpty(request.CreatedName),
                                  pc => pc.CreatedName != null && SqlFunc.Contains(pc.CreatedName, request.CreatedName))
                         .WhereIF(!string.IsNullOrEmpty(request.UpdatedName),
                                  pc => pc.UpdatedName != null && SqlFunc.Contains(pc.UpdatedName, request.UpdatedName))
                         .WhereIF(request.IsAvailable, pc => pc.IsAvailable)
                         .WhereIF(request.FromUpdateTime.HasValue, pc => pc.UpdateTime >= request.FromUpdateTime)
                         .WhereIF(request.ToUpdateTime.HasValue, pc => pc.UpdateTime <= request.ToUpdateTime)
                         .WhereIF(!string.IsNullOrEmpty(request.Note),
                                  pc => pc.Note != null && SqlFunc.Contains(pc.Note, request.Note));

            // Nếu có lọc theo ProductName
            if (!string.IsNullOrEmpty(request.ProductName))
            {
                query = query.Where(pc => SqlFunc.Subqueryable<Product>()
                    .Where(p => p.Id == pc.ProductId && SqlFunc.Contains(p.ProductName, request.ProductName))
                    .Any());
            }

            return await query.ToListAsync();
        }

        public async Task<List<ProductColor>> GetAvailableColorsByProductIdAsync(int productId)
        {
            return await _context.Db.Queryable<ProductColor>()
                .Includes(pc => pc.MediaList)
                .Where(pc => pc.ProductId == productId && pc.IsAvailable)
                .ToListAsync();
        }

        public async Task<List<ProductColor>> GetByProductIdAsync(int productId)
        {
            return await _context.Db.Queryable<ProductColor>()
                .Includes(pc => pc.MediaList)
                .Where(pc => pc.ProductId == productId)
                .ToListAsync();
        }

        public async Task<List<ProductColor>> GetByProductIdAndColorIdAsync(int productId, int colorId)
        {
            return await _context.Db.Queryable<ProductColor>()
                .Includes(pc => pc.MediaList)
                .Where(pc => pc.ProductId == productId && pc.Id == colorId)
                .ToListAsync();
        }

        public async Task<List<ProductColor>> GetAllWithProductAsync()
        {
            return await _context.Db.Queryable<ProductColor>()
                .Includes(pc => pc.Product)
                .ToListAsync();
        }
    }
}
