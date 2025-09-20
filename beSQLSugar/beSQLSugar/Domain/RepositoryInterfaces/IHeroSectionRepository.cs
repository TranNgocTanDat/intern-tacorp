using beSQLSugar.Application.DTOs.request;
using beSQLSugar.Domain.Enities;
using beSQLSugar.Domain.Interfaces;

namespace beSQLSugar.Domain.RepositoryInterfaces
{
    // Tạo repository interface riêng cho HeroSection kế thùa IRepository
    public interface IHeroSectionRepository : IRepository<HeroSection>
    {
        
        Task<List<HeroSection>> FilterAsync(HeroSectionFilterRequest request);

    }
}
