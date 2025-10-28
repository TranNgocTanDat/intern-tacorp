using beSQLSugar.Application.Dto.request.HeroSectionProduct;
using beSQLSugar.Application.Dto.response.HeroSectionProduct;
using System.Security.Claims;

namespace beSQLSugar.Application.Services.HeroSectionProductServices
{
    public interface IHeroSectionProductService
    {
        // CRUD cơ bản cho HeroSectionProduct
        Task<List<HeroSectionProductResponse>> GetAllAsync();
        Task<HeroSectionProductResponse?> GetByIdAsync(int id);
        Task<HeroSectionProductResponse?> AddAsync(HeroSectionProductRequest request, ClaimsPrincipal admin);
        Task<HeroSectionProductResponse> UpdateAsync(int id, HeroSectionProductRequest request, ClaimsPrincipal admin);
        Task<bool> DeleteAsync(int id);

        Task<List<HeroSectionProductResponse>> GetAllHRPAsync();

        // Lấy danh sách HeroSectionProduct theo HeroSectionId
        Task<List<HeroSectionProductResponse>> GetByHeroSectionIdAsync(int heroSectionId);

        // Lấy HeroSectionProduct theo HeroSectionId + ProductId
        Task<HeroSectionProductResponse?> GetByHeroSectionAndProductAsync(int heroSectionId, int productId);

        // Lọc HeroSectionProduct
        Task<List<HeroSectionProductResponse>> FilterAsync(HeroSectionProductFilterRequest filterRequest);
    }
}
