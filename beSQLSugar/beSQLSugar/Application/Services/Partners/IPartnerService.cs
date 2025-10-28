using beSQLSugar.Application.Dto.request.Partner;
using beSQLSugar.Application.Dto.response.Partner;

namespace beSQLSugar.Application.Services.Partners
{
    public interface IPartnerService
    {
        Task<PartnerResponse> CreatePartnerAsync(PartnerRequest request);
        Task<PartnerResponse> GetPartnerByIdAsync(int id);
        Task<List<PartnerResponse>> GetAllPartnersAsync();
        Task<PartnerResponse> UpdatePartnerAsync(int id, PartnerRequest request);
        Task<bool> DeletePartnerAsync(int id);

        Task<List<PartnerResponse>> FilterPartnersAsync(PartnerFilterRequest request);
    }
}
