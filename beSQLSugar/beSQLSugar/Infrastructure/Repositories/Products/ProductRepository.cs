using Azure.Core;
using beSQLSugar.Application.Dto.request.Product;
using beSQLSugar.Infrastructure.Database;
using beSQLSugar.Infrastructure.Database.Enities;
using SqlSugar;

namespace beSQLSugar.Infrastructure.Repositories.Products
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(SqlSugarDbContext context) : base(context)
        {
        }
        public async Task<List<Product>> FilterProductsAsync(ProductFilterRequest filterRequest)
        {
            var query = _context.Db.Queryable<Product>()
                .Includes(p => p.Category);
            if (!string.IsNullOrEmpty(filterRequest.ProductName))
            {
                query = query.Where(p => SqlSugar.SqlFunc.Contains(p.ProductName, filterRequest.ProductName));
            }
            if (!string.IsNullOrEmpty(filterRequest.Slug))
            {
                query = query.Where(p => SqlSugar.SqlFunc.Contains(p.Slug, filterRequest.Slug));
            }
            if (filterRequest.MinPrice.HasValue)
            {
                query = query.Where(p => p.DiscountPrice >= filterRequest.MinPrice.Value);
            }
            if (filterRequest.MaxPrice.HasValue)
            {
                query = query.Where(p => p.DiscountPrice <= filterRequest.MaxPrice.Value);
            }
            if (filterRequest.IsActive.HasValue)
            {
                query = query.Where(p => p.IsActive == filterRequest.IsActive.Value);
            }
            if (!string.IsNullOrEmpty(filterRequest.CreatedName))
            {
                query = query.Where(c => c.CreatedName != null && SqlFunc.Contains(c.CreatedName, filterRequest.CreatedName));
            }

            if (!string.IsNullOrEmpty(filterRequest.UpdatedName))
            {
                query = query.Where(c => c.UpdatedName != null && SqlFunc.Contains(c.UpdatedName, filterRequest.UpdatedName));
            }
            if (!string.IsNullOrEmpty(filterRequest.CategoryName))
            {
                query = query.Where(p => SqlSugar.SqlFunc.Contains(p.Category!.Name, filterRequest.CategoryName));
            }
            if (filterRequest.MinViewsCount.HasValue)
            {
                query = query.Where(p => p.ViewsCount >= filterRequest.MinViewsCount.Value);
            }
            if (filterRequest.MaxViewsCount.HasValue)
            {
                query = query.Where(p => p.ViewsCount <= filterRequest.MaxViewsCount.Value);
            }
            if(!string.IsNullOrEmpty(filterRequest.LongDescription))
            {
                query = query.Where(p => p.LongDescription != null && p.LongDescription.Contains(filterRequest.LongDescription));
            }
            if(!string.IsNullOrEmpty(filterRequest.ShortDescription))
            {
                query = query.Where(p => p.ShortDescription != null && p.ShortDescription.Contains(filterRequest.ShortDescription));
            }
            return await query.ToListAsync();

        }

        public async Task<Product?> GetByNameAsync(string name)
        {
            return await _context.Db.Queryable<Product>()
                .Where(p => p.ProductName == name)
                .FirstAsync();
        }

        public async Task<Product?> GetBySlugAsync(string slug)
        {
            return await _context.Db.Queryable<Product>()
                .Where(p => p.Slug == slug)
                .Includes(p => p.MediaList)
                .Includes(p => p.Specs)
                .Includes(p => p.Colors)
                .Includes(p => p.Storages)
                .FirstAsync();
        }

        public async Task<Product?> GetProductWithDetailsAsync(int id)
        {
            var product = await _context.Db.Queryable<Product>()
                .Where(p => p.Id == id)
                .Includes(p => p.Category)
                .Includes(p => p.MediaList) // load media
                .Includes(p => p.Specs)     // load specs
                       .Includes(p => p.Colors)
        .Includes(p => p.Storages)
                .FirstAsync();
            return product;
        }

        public async Task<List<Product>> GetFeaturedProductsAsync()
        {
            return await _context.Db.Queryable<Product>()
                .Where(p => p.IsFeatured && p.IsActive)
                .Includes(p => p.Category)
                .Includes(p => p.MediaList) // load media
                .Includes(p => p.Specs)     // load specs
                .Includes(p => p.Colors)
                .Includes(p => p.Storages)
                .ToListAsync();
        }
    }
}
