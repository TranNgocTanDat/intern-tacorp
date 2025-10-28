using beSQLSugar.Application.Dto.request.HeroSection;
using beSQLSugar.Application.Dto.response.HeroSection;
using System.Security.Claims;

namespace beSQLSugar.Application.Services.HeroSectionServices
{
    public interface IHeroSectionService
    {
        // Các phương thức CRUD cơ bản

        Task<HeroSectionResponse?> AddAsync(HeroSectionRequest request, ClaimsPrincipal admin);
        Task<HeroSectionResponse> UpdateAsync(int id, HeroSectionRequest request, ClaimsPrincipal admin);
        Task<bool> DeleteAsync(int id);

        // Các phương thức riêng cho HeroSection

        Task<List<HeroSectionResponse>> FilterAsync(HeroSectionFilterRequest request);

        // Lấy HeroSection kèm HeroProducts
        Task<HeroSectionResponse?> GetHeroSectionWithDetailsAsync(int id);
        Task<List<HeroSectionResponse>> GetAllWithDetailsAsync();

        Task<List<HeroSectionResponse>> GetHeroSectionsWithPageHeroAsync(string pageHero);
    }
}
