using beSQLSugar.Application.Dto.request.Partner;
using beSQLSugar.Infrastructure.Database.Enities;

namespace beSQLSugar.Infrastructure.Repositories.Partners
{
    public interface IPartnerRepository : IRepository<Partner>
    {
        Task<List<Partner>> FilterPartnerAsync(PartnerFilterRequest request);
    }
}
