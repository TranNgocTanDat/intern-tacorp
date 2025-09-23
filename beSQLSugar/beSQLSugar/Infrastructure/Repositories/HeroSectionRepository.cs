using beSQLSugar.Application.DTOs.request;
using beSQLSugar.Domain.Enities;
using beSQLSugar.Domain.RepositoryInterfaces;
using beSQLSugar.Infrastructure.Database;

namespace beSQLSugar.Infrastructure.Repositories
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

    }
}
