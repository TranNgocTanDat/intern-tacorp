using SqlSugar;
namespace beSQLSugar.Infrastructure.Database.Enities
{
    [SugarTable("partners")]
    public class Partner
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(ColumnDataType = "NVARCHAR(200)", IsNullable = false)]
        public string Name { get; set; } = string.Empty;

        [SugarColumn(Length = 1000, IsNullable = true)]
        public string? LogoUrl { get; set; }
        [SugarColumn(Length = 1000, IsNullable = true)]
        public string? ImgDefaultUrl { get; set; }
        [SugarColumn(Length = 1000, IsNullable = true)]
        public string? ImgHoverUrl { get; set; }
        [SugarColumn(ColumnDataType = "NVARCHAR(50)", IsNullable = true)]
        public string? slug { get; set; }

        [SugarColumn(ColumnDataType = "NVARCHAR(500)", IsNullable = true)]
        public string? Link { get; set; }

        [SugarColumn(DefaultValue = "0")]
        public int OrderIndex { get; set; }

        [SugarColumn(DefaultValue = "1")]
        public bool IsActive { get; set; }

        [SugarColumn(IsNullable = true)]
        public int? CreateUid { get; set; }

        [SugarColumn(ColumnDataType = "NVARCHAR(100)", IsNullable = true)]
        public string? CreatedName { get; set; }

        [SugarColumn(IsNullable = true)]
        public int? WriteIUid { get; set; }

        [SugarColumn(ColumnDataType = "NVARCHAR(100)", IsNullable = true)]
        public string? UpdatedName { get; set; }

        [SugarColumn(IsNullable = true)]
        public DateTime? UpdateTime { get; set; }

        [SugarColumn(ColumnDataType = "NVARCHAR(250)", IsNullable = true)]
        public string? Note { get; set; }

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
