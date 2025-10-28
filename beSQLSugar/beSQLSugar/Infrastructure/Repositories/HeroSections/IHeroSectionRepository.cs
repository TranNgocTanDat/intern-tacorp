using beSQLSugar.Application.Dto.request.HeroSection;
using beSQLSugar.Infrastructure.Database.Enities;
using beSQLSugar.Infrastructure.Repositories;

namespace beSQLSugar.Infrastructure.Repositories.HeroSections
{
    // Tạo repository interface riêng cho HeroSection kế thùa IRepository
    public interface IHeroSectionRepository : IRepository<HeroSection>
    {
        
        Task<List<HeroSection>> FilterAsync(HeroSectionFilterRequest request);

        // Lấy HeroSection kèm HeroProducts theo id
        Task<HeroSection?> GetHeroSectionWithDetailsAsync(int id);

        // Lấy toàn bộ HeroSection kèm HeroProducts
        Task<List<HeroSection>> GetAllWithDetailsAsync();

        // Lấy danh sách HeroSection kèm HeroProducts theo pageHero và isPublished = true
        Task<List<HeroSection>> GetHeroSectionsWithPageHeroAsync(string pageHero);


    }
}
