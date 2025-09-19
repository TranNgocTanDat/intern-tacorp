using beSQLSugar.Domain.Enities;
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
        }
    }
}
