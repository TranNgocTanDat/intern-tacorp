using SqlSugar;

namespace beSQLSugar.Infrastructure.Database.Enities
{
    [SugarTable("product_storage")]
    public class ProductStorage
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(IsNullable = false)]
        public int ProductId { get; set; }

        [SugarColumn(ColumnDataType = "NVARCHAR(50)", IsNullable = false)]
        public string StorageName { get; set; } = string.Empty; // ví dụ: "128GB", "256GB"

        [SugarColumn(IsNullable = true)]
        public decimal? AdditionalPrice { get; set; } // cộng thêm so với giá gốc

        [SugarColumn(DefaultValue = "1")]
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
    }
}
