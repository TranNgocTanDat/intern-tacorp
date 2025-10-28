using beSQLSugar.Application.Dto.request.ProductSpec;
using beSQLSugar.Infrastructure.Database;
using beSQLSugar.Infrastructure.Database.Enities;
using SqlSugar;

namespace beSQLSugar.Infrastructure.Repositories.ProductSpecs
{
    public class ProductSpecRepository : BaseRepository<ProductSpec>, IProductSpecRepository
    {
       
        public ProductSpecRepository(SqlSugarDbContext context) : base(context)
        {
        }

        public async Task<List<ProductSpec>> FilterProductSpecs(ProductSpecFilterRequest request)
        {
            var query = _context.Db.Queryable<ProductSpec>()
                                   .Includes(m => m.Product);
                                   

            query = query.WhereIF(request.ProductId.HasValue, ps => ps.ProductId == request.ProductId!.Value)
                         .WhereIF(!string.IsNullOrEmpty(request.SpecKey),
                                  ps => ps.SpecKey != null && SqlFunc.Contains(ps.SpecKey, request.SpecKey))
                         .WhereIF(!string.IsNullOrEmpty(request.SpecValue),
                                  ps => ps.SpecValue != null && SqlFunc.Contains(ps.SpecValue, request.SpecValue))
                         .WhereIF(!string.IsNullOrEmpty(request.CreatedName),
                                  ps => ps.CreatedName != null && SqlFunc.Contains(ps.CreatedName, request.CreatedName))
                         .WhereIF(!string.IsNullOrEmpty(request.UpdatedName),
                                  ps => ps.UpdatedName != null && SqlFunc.Contains(ps.UpdatedName, request.UpdatedName))
                         .WhereIF(request.FromUpdateTime.HasValue,
                                  ps => ps.UpdateTime >= request.FromUpdateTime!.Value)
                         .WhereIF(request.ToUpdateTime.HasValue,
                                  ps => ps.UpdateTime <= request.ToUpdateTime!.Value);

            // Nếu bạn muốn filter theo ProductName thì phải join với bảng Product
            if (!string.IsNullOrEmpty(request.ProductName))
            {
                query = query.Where(m => SqlFunc.Subqueryable<Product>()
                                      .Where(p => p.Id == m.ProductId && p.ProductName!.Contains(request.ProductName))
                                      .Any());
            }

            return await query.ToListAsync();
        }

        public async Task<List<ProductSpec>> GetAllWithProductAsync()
        {
            return await _context.Db.Queryable<ProductSpec>()
                                     .Includes(ps => ps.Product) // load Product
                                     .ToListAsync();
        }

        public async Task<List<ProductSpec>> GetByProductIdAsync(int productId)
        {
            return await _context.Db.Queryable<ProductSpec>()
                                    .Where(s => s.ProductId == productId)
                                    .ToListAsync();
        }
    }
}
