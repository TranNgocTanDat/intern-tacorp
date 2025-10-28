using beSQLSugar.Application.Dto.request.HeroSectionProduct;
using beSQLSugar.Infrastructure.Database.Enities;
using beSQLSugar.Infrastructure.Repositories;

namespace beSQLSugar.Infrastructure.Repositories.HeroSectionProducts
{
    public interface IHeroSectionProductRepository : IRepository<HeroSectionProduct>
    {
        Task<List<HeroSectionProduct>> GetAllHRPAsync();
        // Lấy danh sách HeroSectionProduct theo HeroSectionId
        Task<List<HeroSectionProduct>> GetByHeroSectionIdAsync(int heroSectionId);
       
        // Lấy HeroSectionProduct theo HeroSectionId + ProductId (nếu cần check trùng lặp)
        Task<HeroSectionProduct?> GetByHeroSectionAndProductAsync(int heroSectionId, int productId);

        // Lọc HeroSectionProduct 
        Task<List<HeroSectionProduct>> FilterAsync(HeroSectionProductFilterRequest filterRequest);
    }
}
