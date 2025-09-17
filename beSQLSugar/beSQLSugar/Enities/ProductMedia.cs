using SqlSugar;
namespace beSQLSugar.Models
{
    [SugarTable("product_media")]
    public class ProductMedia
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(IsNullable = false)]
        public int ProductId { get; set; }

        [SugarColumn(Length = 1250)]
        public string? MediaFileUrl { get; set; }

        [SugarColumn(Length = 20)]
        public string? MediaType { get; set; } // "image" or "video"

        [SugarColumn(DefaultValue = "0")]
        public bool IsPrimary { get; set; } = false;

        [SugarColumn(DefaultValue = "0")]
        public int OrderIndex { get; set; } = 0;

        [SugarColumn(IsNullable = true)]
        public int? CreateUid { get; set; }

        [SugarColumn(IsNullable = true)]
        public int? WriteIUid { get; set; }

        [SugarColumn(IsNullable = true)]
        public DateTime? UpdateTime { get; set; }

        [SugarColumn(Length = 250, IsNullable = true)]
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
