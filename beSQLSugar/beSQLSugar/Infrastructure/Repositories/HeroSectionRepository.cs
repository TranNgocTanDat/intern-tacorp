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

        // Lấy danh sách HeroSection theo khoảng thời gian
        public async Task<List<HeroSection>> GetByDateRangeAsync(DateTime from, DateTime to)
        {
            return await _context.Db.Queryable<HeroSection>()
                .Where(h => (h.PublishFrom == null || h.PublishFrom >= from) &&
                            (h.PublishTo == null || h.PublishTo <= to))
                .ToListAsync();
        }

        // Tìm HeroSection theo pageHero
        public async Task<List<HeroSection>> GetByPageHeroAsync(string pageHero)
        {
            return await _context.Db.Queryable<HeroSection>()
                .Where(h => h.PageHero == pageHero)
                .ToListAsync();
        }

        // Lấy danh sách HeroSection đang được public
        public async Task<List<HeroSection>> GetPublishedAsync()
        {
            // Sử dụng SQLSugar để truy vấn dữ liệu
            return await _context.Db.Queryable<HeroSection>()
                .Where(h => h.IsPublished && 
                            (h.PublishFrom == null || h.PublishFrom <= DateTime.Now) &&
                            (h.PublishTo == null || h.PublishTo >= DateTime.Now))
                .ToListAsync();
        }

        // Lấy HeroSection cùng với danh sách HeroProduct liên quan
        public async Task<HeroSection?> GetWithProductsAsync(int id)
        {
            return await _context.Db.Queryable<HeroSection>()
                .Mapper(h => h.HeroProducts, h => h.Id, h => h.HeroProducts!.First().HeroSectionId) // map quan hệ
                .FirstAsync(h => h.Id == id);
        }

        // Tìm kiếm HeroSection theo từ khóa trong title
        public async Task<List<HeroSection>> SearchAsync(string keyword)
        {
            return await _context.Db.Queryable<HeroSection>()
                .WhereIF(!string.IsNullOrEmpty(keyword), h => h.Title!.Contains(keyword))
                .ToListAsync();
        }
    }
}
