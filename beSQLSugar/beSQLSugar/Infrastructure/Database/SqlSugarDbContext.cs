using beSQLSugar.Infrastructure.Database.Enities;
using Microsoft.Extensions.Configuration;
using SqlSugar;

namespace beSQLSugar.Infrastructure.Database
{
    public class SqlSugarDbContext
    {
        public SqlSugarClient Db { get; }

        public SqlSugarDbContext(IConfiguration configuration)
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = configuration.GetConnectionString("DefaultConnection"),
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });

            Db.CodeFirst.InitTables(typeof(AdminUser));
            Db.CodeFirst.InitTables(typeof(HeroSection));
            Db.CodeFirst.InitTables(typeof(Category));
            Db.CodeFirst.InitTables(typeof(Product));
            Db.CodeFirst.InitTables(typeof(ProductMedia));
            Db.CodeFirst.InitTables(typeof(ProductSpec));
            Db.CodeFirst.InitTables(typeof(HeroSectionProduct));
            Db.CodeFirst.InitTables(typeof(Contact));
            Db.CodeFirst.InitTables(typeof(Partner));
            Db.CodeFirst.InitTables(typeof(ProductColor));
            Db.CodeFirst.InitTables(typeof(ProductStorage));
            Db.CodeFirst.InitTables(typeof(AnalyzedImage));
        }
    }
}
