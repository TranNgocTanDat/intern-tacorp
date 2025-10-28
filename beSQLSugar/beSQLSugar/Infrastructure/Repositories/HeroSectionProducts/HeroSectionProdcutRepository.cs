using Azure.Core;
using beSQLSugar.Application.Dto.request.HeroSectionProduct;
using beSQLSugar.Application.Dto.response.HeroSectionProduct;
using beSQLSugar.Infrastructure.Database;
using beSQLSugar.Infrastructure.Database.Enities;
using SqlSugar;

namespace beSQLSugar.Infrastructure.Repositories.HeroSectionProducts
{
    public class HeroSectionProductRepository : BaseRepository<HeroSectionProduct>, IHeroSectionProductRepository
    {
        public HeroSectionProductRepository(SqlSugarDbContext context) : base(context)
        {
        }

        // Lấy danh sách HeroSectionProduct chứa Product
        public async Task<List<HeroSectionProduct>> GetAllHRPAsync()
        {
            return await _context.Db.Queryable<HeroSectionProduct>()
                .Includes(hp => hp.HeroSection)
                .Includes(hp => hp.Product)
                .Includes(hp => hp.Product!.MediaList)
                .Includes(hp => hp.Product!.Specs)
                .ToListAsync();
        }

        // Lấy danh sách HeroSectionProduct theo HeroSectionId
        public async Task<List<HeroSectionProduct>> GetByHeroSectionIdAsync(int heroSectionId)
        {
            return await _context.Db.Queryable<HeroSectionProduct>()
                .Includes(hp => hp.Product) // load thêm Product
                        .Includes(hp => hp.Product!.MediaList)
        .Includes(hp => hp.Product!.Specs)
                .Where(hp => hp.HeroSectionId == heroSectionId)
                .ToListAsync();
        }

        // Lấy HeroSectionProduct theo HeroSectionId + ProductId (check trùng lặp)
        public async Task<HeroSectionProduct?> GetByHeroSectionAndProductAsync(int heroSectionId, int productId)
        {
            return await _context.Db.Queryable<HeroSectionProduct>()
                .FirstAsync(hp => hp.HeroSectionId == heroSectionId && hp.ProductId == productId);
        }

        // Lọc HeroSectionProduct
        public async Task<List<HeroSectionProduct>> FilterAsync(HeroSectionProductFilterRequest filterRequest)
        {
            // Khởi tạo query với JOIN HeroSection và Product
            var query = _context.Db.Queryable<HeroSectionProduct>()
                .LeftJoin<HeroSection>((hp, hs) => hp.HeroSectionId == hs.Id)
                .LeftJoin<Product>((hp, hs, p) => hp.ProductId == p.Id);

            // Filter theo HeroSection.Title
            if (!string.IsNullOrEmpty(filterRequest.HeroSectionTitle))
                query = query.Where((hp, hs, p) => hs.Title!.Contains(filterRequest.HeroSectionTitle));

            // Filter theo Product.ProductName
            if (!string.IsNullOrEmpty(filterRequest.ProductName))
                query = query.Where((hp, hs, p) => p.ProductName!.Contains(filterRequest.ProductName));

            if (!string.IsNullOrEmpty(filterRequest.CreatedName))
                query = query.Where((hp, hs, p) => hp.CreatedName!.Contains(filterRequest.CreatedName));

            if (!string.IsNullOrEmpty(filterRequest.UpdatedName))
                query = query.Where((hp, hs, p) => hp.UpdatedName!.Contains(filterRequest.UpdatedName));

            if (filterRequest.UpdateTimeFrom.HasValue)
                query = query.Where((hp, hs, p) => hp.UpdateTime >= filterRequest.UpdateTimeFrom.Value);

            if (filterRequest.UpdateTimeTo.HasValue)
                query = query.Where((hp, hs, p) => hp.UpdateTime <= filterRequest.UpdateTimeTo.Value);

            if (!string.IsNullOrEmpty(filterRequest.Note))
                query = query.Where((hp, hs, p) => hp.Note.Contains(filterRequest.Note));

            // Lấy HeroSectionProduct (hp)
            var result = await query.Select((hp, hs, p) => hp).ToListAsync();

            // Gắn navigation thủ công (Include Product + HeroSection)
            _context.Db.ThenMapper(result, it =>
            {
                it.HeroSection = _context.Db.Queryable<HeroSection>()
                    .First(x => x.Id == it.HeroSectionId);

                it.Product = _context.Db.Queryable<Product>()
                    .First(x => x.Id == it.ProductId);
            });

            return result;
        }

    }
}
