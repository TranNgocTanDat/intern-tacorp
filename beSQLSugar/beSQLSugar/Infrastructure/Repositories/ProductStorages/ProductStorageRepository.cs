using beSQLSugar.Application.Dto.request.ProductStorage;
using beSQLSugar.Infrastructure.Database;
using beSQLSugar.Infrastructure.Database.Enities;
using SqlSugar;

namespace beSQLSugar.Infrastructure.Repositories.ProductStorages
{
    public class ProductStorageRepository : BaseRepository<ProductStorage>, IProductStorageRepository
    {
        public ProductStorageRepository(SqlSugarDbContext context) : base(context)
        {

        }

        public async Task<List<ProductStorage>> FilterProductStorageAsync(ProductStorageFilterRequest request)
        {
            var query = _context.Db.Queryable<ProductStorage>()
                                   .Includes(ps => ps.Product);

            query = query.WhereIF(request.ProductId > 0, ps => ps.ProductId == request.ProductId)
                         .WhereIF(!string.IsNullOrEmpty(request.StorageName),
                                  ps => SqlFunc.Contains(ps.StorageName, request.StorageName))
                         .WhereIF(request.AdditionalPrice.HasValue,
                                  ps => ps.AdditionalPrice == request.AdditionalPrice)
                         .WhereIF(!string.IsNullOrEmpty(request.CreatedName),
                                  ps => ps.CreatedName != null && SqlFunc.Contains(ps.CreatedName, request.CreatedName))
                         .WhereIF(!string.IsNullOrEmpty(request.UpdatedName),
                                  ps => ps.UpdatedName != null && SqlFunc.Contains(ps.UpdatedName, request.UpdatedName))
                         .WhereIF(request.FromUpdateTime.HasValue,
                                  ps => ps.UpdateTime >= request.FromUpdateTime)
                         .WhereIF(request.ToUpdateTime.HasValue,
                                  ps => ps.UpdateTime <= request.ToUpdateTime)
                         .WhereIF(!string.IsNullOrEmpty(request.Note),
                                  ps => ps.Note != null && SqlFunc.Contains(ps.Note, request.Note));

            // Lọc theo ProductName
            if (!string.IsNullOrEmpty(request.ProductName))
            {
                query = query.Where(ps => SqlFunc.Subqueryable<Product>()
                    .Where(p => p.Id == ps.ProductId && SqlFunc.Contains(p.ProductName, request.ProductName))
                    .Any());
            }

            return await query.ToListAsync();
        }

        public async Task<List<ProductStorage>> GetAllWithProductAsync()
        {
            return await _context.Db.Queryable<ProductStorage>()
                .Includes(ps => ps.Product)
                .ToListAsync();
        }

        public async Task<List<ProductStorage>> GetByProductIdAsync(int productId)
        {
            return await _context.Db.Queryable<ProductStorage>()
                .Where(ps => ps.ProductId == productId)
                .ToListAsync();
        }
    }
}
