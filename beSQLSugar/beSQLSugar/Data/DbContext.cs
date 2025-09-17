using beSQLSugar.models;
using Microsoft.Extensions.Configuration;
using SqlSugar;

namespace beSQLSugar.Data
{
    public class DbContext
    {
        public SqlSugarClient Db { get; }

        public DbContext(IConfiguration configuration)
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = configuration.GetConnectionString("DefaultConnection"),
                DbType = SqlSugar.DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            });

            Db.CodeFirst.InitTables(typeof(DeviceType));
        }
    }
}
