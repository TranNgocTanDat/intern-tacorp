using SqlSugar;
namespace beSQLSugar.Models
{
    [SugarTable("contact")]
    public class Contact
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(Length = 200, IsNullable = false)]
        public string Fullname { get; set; } = string.Empty;

        [SugarColumn(Length = 200, IsNullable = true)]
        public string? Email { get; set; }

        [SugarColumn(Length = 20, IsNullable = true)]
        public string? Phone { get; set; }

        [SugarColumn(Length = 500, IsNullable = true)]
        public string? Address { get; set; }

        [SugarColumn(IsNullable = true, ColumnDataType = "nvarchar(max)")]
        public string? UserNote { get; set; }

        [SugarColumn(IsNullable = true)]
        public int? ProductId { get; set; }

        [SugarColumn(Length = 50, IsNullable = true)]
        public string? Status { get; set; }

        [SugarColumn(IsNullable = false, ColumnDataType = "datetime2", DefaultValue = "SYSDATETIME()")]
        public DateTime CreateTime { get; set; }

        [SugarColumn(IsNullable = true)]
        public int? HandleByAdminId { get; set; }

        [SugarColumn(IsNullable = true)]
        public DateTime? HandleTime { get; set; }

        [SugarColumn(Length = 250, IsNullable = true)]
        public string? AdminNote { get; set; }

        [SugarColumn(IsNullable = true)]
        public int? WriteIUid { get; set; }

        [SugarColumn(IsNullable = true)]
        public DateTime? UpdateTime { get; set; }

        [SugarColumn(Length = 120, IsNullable = true)]
        public string? Option1 { get; set; }

        [SugarColumn(Length = 120, IsNullable = true)]
        public string? Option2 { get; set; }

        [SugarColumn(Length = 120, IsNullable = true)]
        public string? Option3 { get; set; }

        [SugarColumn(Length = 120, IsNullable = true)]
        public string? Option4 { get; set; }

        [SugarColumn(Length = 120, IsNullable = true)]
        public string? Option5 { get; set; }
    }
}
