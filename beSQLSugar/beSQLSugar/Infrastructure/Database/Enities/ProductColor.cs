using SqlSugar;

namespace beSQLSugar.Infrastructure.Database.Enities
{
    [SugarTable("product_color")]
    public class ProductColor
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(IsNullable = false)]
        public int ProductId { get; set; }

        [SugarColumn(ColumnDataType = "NVARCHAR(100)", IsNullable = false)]
        public string ColorName { get; set; } = string.Empty; // ví dụ: "Trắng Titan"

        [SugarColumn(ColumnDataType = "NVARCHAR(10)", IsNullable = true)]
        public string? ColorCode { get; set; } // ví dụ: "#FFFFFF"

        [SugarColumn(IsNullable = true)]
        public bool IsAvailable { get; set; } = true;
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

        [SugarColumn(ColumnDataType = "NVARCHAR(500)", IsNullable = true)]
        public string? Note { get; set; }

        [Navigate(NavigateType.ManyToOne, nameof(ProductId))]
        public Product? Product { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(ProductMedia.ColorId))]
        public List<ProductMedia>? MediaList { get; set; }
    }
}
