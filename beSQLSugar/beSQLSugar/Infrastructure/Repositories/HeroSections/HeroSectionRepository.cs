using beSQLSugar.Application.Dto.request.HeroSection;
using beSQLSugar.Infrastructure.Database;
using beSQLSugar.Infrastructure.Database.Enities;

namespace beSQLSugar.Infrastructure.Repositories.HeroSections
{
    // Triển khai repository riêng cho HeroSection
    // Kế thùa BaseRepository để sử dụng các phương thức CRUD chung và triển khai IHeroSetionRepository
    public class HeroSectionRepository : BaseRepository<HeroSection>, IHeroSectionRepository
    {
        public HeroSectionRepository(SqlSugarDbContext context) : base(context)
        {
        }

        // Filter HeroSection theo các tiêu chí trong request
        public async Task<List<HeroSection>> FilterAsync(HeroSectionFilterRequest request)
        {
            var query = _context.Db.Queryable<HeroSection>();

            if (!string.IsNullOrEmpty(request.Title))
            {
                query = query.Where(h => h.Title != null && h.Title.Contains(request.Title));
            }

            if (!string.IsNullOrEmpty(request.Description))
            {
                query = query.Where(h => h.Description != null && h.Description.Contains(request.Description));
            }

            if (!string.IsNullOrEmpty(request.PageHero))
            {
                query = query.Where(h => h.PageHero != null && h.PageHero.Contains(request.PageHero));
            }

            if (!string.IsNullOrEmpty(request.CreatedName))
            {
                query = query.Where(h => h.CreatedName != null && h.CreatedName.Contains(request.CreatedName));
            }

            if (!string.IsNullOrEmpty(request.UpdatedName))
            {
                query = query.Where(h => h.UpdatedName != null && h.UpdatedName.Contains(request.UpdatedName));
            }

            if (request.IsPublished.HasValue)
            {
                query = query.Where(h => h.IsPublished == request.IsPublished.Value);
            }

            if (request.PublishFrom.HasValue)
            {
                // record có ngày bắt đầu <= PublishFrom
                query = query.Where(h => h.PublishFrom != null && h.PublishFrom >= request.PublishFrom.Value);
            }

            if (request.PublishTo.HasValue)
            {
                // record có ngày kết thúc >= PublishTo
                query = query.Where(h => h.PublishTo != null && h.PublishTo <= request.PublishTo.Value);
            }

            return await query.ToListAsync();
        }

        // Lấy HeroSection kèm HeroProducts theo pageHero
        public async Task<HeroSection?> GetHeroSectionWithDetailsAsync(int id)
        {
            return await _context.Db.Queryable<HeroSection>()
                .Includes(h => h.HeroProducts, hp => hp.Product) // load cả HeroProducts và Product
                .FirstAsync(h => h.Id == id);
        }

        // Lấy toàn bộ HeroSection kèm HeroProducts
        public async Task<List<HeroSection>> GetAllWithDetailsAsync()
        {
            return await _context.Db.Queryable<HeroSection>()
                .Includes(h => h.HeroProducts, hp => hp.Product)
                .ToListAsync();
        }

        // Lấy danh sách HeroSection theo pageHero
        public async Task<List<HeroSection>> GetHeroSectionsWithPageHeroAsync(string pageHero)
        {
            return await _context.Db.Queryable<HeroSection>()
                .Includes(h => h.HeroProducts, hp => hp.Product, p => p.MediaList)
                .Includes(h => h.HeroProducts, hp => hp.Product, p => p.Specs)
                .Where(h => h.PageHero == pageHero && h.IsPublished)
                .ToListAsync();
        }



    }
}
