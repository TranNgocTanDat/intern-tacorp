using beSQLSugar.Application.Dto.request.ProductMedia;
using beSQLSugar.Infrastructure.Database;
using beSQLSugar.Infrastructure.Database.Enities;
using SqlSugar;

namespace beSQLSugar.Infrastructure.Repositories.ProductMedias
{
    public class ProductMediaRepository : BaseRepository<ProductMedia>, IProductMediaRepository
    {
        public ProductMediaRepository(SqlSugarDbContext context) : base(context)
        {
        }

        public async Task<List<ProductMedia>> GetAllWithProductAsync()
        {
            return await _context.Db.Queryable<ProductMedia>()
                .Includes(pm => pm.Product) // load Product
                .Includes(pm => pm.ProductColor)
                .ToListAsync();
        }

        public async Task<List<ProductMedia>> GetByProductIdAsync(int productId)
        {
            return await _context.Db.Queryable<ProductMedia>()
                                    .Where(m => m.ProductId == productId)
                                    .Includes(pm => pm.ProductColor)
                                    .ToListAsync();
        }

        public async Task<List<ProductMedia>> FilterProductMedia(ProductMediaFilterRequest request)
        {
            var query = _context.Db.Queryable<ProductMedia>()
                                    .Includes(m => m.Product); ;

            query = query.WhereIF(request.ProductId.HasValue, m => m.ProductId == request.ProductId!.Value)
                         .WhereIF(!string.IsNullOrEmpty(request.MediaType), m => m.MediaType == request.MediaType)
                         .WhereIF(!string.IsNullOrEmpty(request.DescriptionMedia), m => m.DescriptionMedia == request.DescriptionMedia)
                         .WhereIF(!string.IsNullOrEmpty(request.CreatedName),
                                  ps => ps.CreatedName != null && SqlFunc.Contains(ps.CreatedName, request.CreatedName))
                         .WhereIF(!string.IsNullOrEmpty(request.UpdatedName),
                                  ps => ps.UpdatedName != null && SqlFunc.Contains(ps.UpdatedName, request.UpdatedName))
                         .WhereIF(request.IsPrimary.HasValue, m => m.IsPrimary == request.IsPrimary!.Value)
                         .WhereIF(request.FromUpdateTime.HasValue, m => m.UpdateTime >= request.FromUpdateTime!.Value)
                         .WhereIF(request.ToUpdateTime.HasValue, m => m.UpdateTime <= request.ToUpdateTime!.Value);

            // Nếu bạn muốn filter theo ProductName thì phải join với bảng Product
            if (!string.IsNullOrEmpty(request.ProductName))
            {
                query = query.Where(m => SqlFunc.Subqueryable<Product>()
                                      .Where(p => p.Id == m.ProductId && p.ProductName!.Contains(request.ProductName))
                                      .Any());
            }

            return await query.ToListAsync();
        }
    }
}
