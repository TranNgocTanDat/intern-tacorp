using SqlSugar;
namespace beSQLSugar.Infrastructure.Database.Enities
{
    [SugarTable("admin_users")]
    public class AdminUser
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(ColumnDataType = "NVARCHAR(100)", IsNullable = false)]
        public string Username { get; set; } = string.Empty;

        [SugarColumn(Length = 256, IsNullable = false)]
        public string PasswordHash { get; set; } = string.Empty;

        [SugarColumn(ColumnDataType = "NVARCHAR(150)")]
        public string? FullName { get; set; }

        [SugarColumn(ColumnDataType = "NVARCHAR(200)")]
        public string? Email { get; set; }

        [SugarColumn(ColumnDataType = "NVARCHAR(20)")]
        public string? Phone { get; set; }

        [SugarColumn(DefaultValue = "1")]
        public bool IsActive { get; set; }

        [SugarColumn(Length = 50, IsNullable = false, DefaultValue = "Admin")]
        public string Role { get; set; } = "Admin";

    }
}
