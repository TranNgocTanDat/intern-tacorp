using beSQLSugar.Application.Dto.request.Partner;
using beSQLSugar.Infrastructure.Database;
using beSQLSugar.Infrastructure.Database.Enities;

namespace beSQLSugar.Infrastructure.Repositories.Partners
{
    public class PartnerRepository : BaseRepository<Partner>, IPartnerRepository
    {
        public PartnerRepository(SqlSugarDbContext context) : base(context)
        {
        }

        public async Task<List<Partner>> FilterPartnerAsync(PartnerFilterRequest request)
        {
            var query = _context.Db.Queryable<Partner>();
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(p => SqlSugar.SqlFunc.Contains(p.Name, request.Name));
            }
            if (request.IsActive.HasValue)
            {
                query = query.Where(p => p.IsActive == request.IsActive.Value);
            }
            if (!string.IsNullOrEmpty(request.CreatedName))
            {
                query = query.Where(p => p.CreatedName != null && SqlSugar.SqlFunc.Contains(p.CreatedName, request.CreatedName));
            }
            if (!string.IsNullOrEmpty(request.UpdatedName))
            {
                query = query.Where(p => p.UpdatedName != null && SqlSugar.SqlFunc.Contains(p.UpdatedName, request.UpdatedName));
            }
            if (request.UpdateTimeFrom.HasValue)
            {
                query = query.Where(p => p.UpdateTime >= request.UpdateTimeFrom.Value);
            }
            if (request.UpdateTimeTo.HasValue)
            {
                query = query.Where(p => p.UpdateTime <= request.UpdateTimeTo.Value);
            }
            if (!string.IsNullOrEmpty(request.Note))
            {
                query = query.Where(p => p.Note != null && SqlSugar.SqlFunc.Contains(p.Note, request.Note));
            }
            return await query.ToListAsync();
        }
    }
}
