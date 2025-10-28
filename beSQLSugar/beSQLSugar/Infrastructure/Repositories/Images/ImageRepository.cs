using beSQLSugar.Infrastructure.Database;
using beSQLSugar.Infrastructure.Database.Enities;

namespace beSQLSugar.Infrastructure.Repositories.Images
{
    public class ImageRepository : BaseRepository<AnalyzedImage>, IImageRepository
    {
        public ImageRepository(SqlSugarDbContext context) : base(context)
        {
        }
    }
}
