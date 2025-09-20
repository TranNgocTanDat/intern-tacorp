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
                query = query.Where(h => h.Title == request.Title);
            }
            if (request.IsPublished.HasValue)
            {
                query = query.Where(h => h.IsPublished == request.IsPublished.Value);
            }
            if (!string.IsNullOrEmpty(request.Description))
            {
                query = query.Where(h => h.Description == request.Description);
            }
            if (!string.IsNullOrEmpty(request.PageHero))
            {
                query = query.Where(h => h.PageHero == request.PageHero);
            }
            if (request.PublishFrom.HasValue)
            {
                query = query.Where(h => h.PublishFrom >= request.PublishFrom);
            }
            if (request.PublishTo.HasValue)
            {
                query = query.Where(h => h.PublishTo <= request.PublishTo);
            }
            return await query.ToListAsync();
        }
    }
}
