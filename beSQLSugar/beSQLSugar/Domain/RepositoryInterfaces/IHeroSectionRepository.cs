using beSQLSugar.Domain.Enities;
using beSQLSugar.Domain.Interfaces;

namespace beSQLSugar.Domain.RepositoryInterfaces
{
    // Tạo repository interface riêng cho HeroSection kế thùa IRepository
    public interface IHeroSectionRepository : IRepository<HeroSection>
    {
        // Danh sách HeroSection đang được publish
        Task<List<HeroSection>> GetPublishedAsync();

        // Danh sách HeroSection theo khoảng thời gian
        Task<List<HeroSection>> GetByDateRangeAsync(DateTime from, DateTime to);

        //Tìm HeroSection theo pageHero
        Task<List<HeroSection>> GetByPageHeroAsync(string pageHero);

        // HeroSection với danh sách HeroProduct liên quan
        Task<HeroSection?> GetWithProductsAsync(int id);

        // Tìm kiếm HeroSection theo từ khóa
        Task<List<HeroSection>> SearchAsync(string keyword);
    }
}
