using SqlSugar;
namespace beSQLSugar.Models
{
    [SugarTable("admin_users")]
    public class AdminUser
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(Length = 100, IsNullable = false)]
        public string Username { get; set; } = string.Empty;

        [SugarColumn(Length = 256, IsNullable = false)]
        public string PasswordHash { get; set; } = string.Empty;

        [SugarColumn(Length =150)]
        public string? FullName { get; set; }

        [SugarColumn(Length = 200)]
        public string? Email { get; set; }

        [SugarColumn(Length =20)]
        public string? Phone { get; set; }

        [SugarColumn(DefaultValue = "1")]
        public bool IsActive { get; set; }


    }
}
