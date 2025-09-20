using beSQLSugar.Application.DTO.request;
using beSQLSugar.Application.DTO.response;
using beSQLSugar.Application.DTOs.request;

namespace beSQLSugar.Application.ServiceInterfaces
{
    public interface IHeroSectionService
    {
        // Các phương thức CRUD cơ bản
        Task<List<HeroSectionResponse>> GetAllAsync();
        Task<HeroSectionResponse?> GetByIdAsync(int id);
        Task<HeroSectionResponse?> AddAsync(HeroSectionRequest request);
        Task<HeroSectionResponse> UpdateAsync(int id, HeroSectionRequest request);
        Task<Boolean> DeleteAsync(int id);

        // Các phương thức riêng cho HeroSection

        Task<List<HeroSectionResponse>> FilterAsync(HeroSectionFilterRequest request);
    }
}
