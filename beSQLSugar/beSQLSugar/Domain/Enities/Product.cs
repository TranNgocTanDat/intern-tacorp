using SqlSugar;
namespace beSQLSugar.Domain.Enities
{
    [SugarTable("products")]
    public class Product
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(IsNullable = false)]
        public int CategoryId { get; set; } // liên kết với Category
         
        [SugarColumn(Length = 250, IsNullable = false)]
        public string ProductName { get; set; } = string.Empty;

        [SugarColumn(Length = 250, IsNullable = false)]
        public string Slug { get; set; } = string.Empty;

        [SugarColumn(Length = 500)]
        public string? ShortDescription { get; set; }

        [SugarColumn(ColumnDataType = "nvarchar(max)")]
        public string? LongDescription { get; set; }

        [SugarColumn(IsNullable = true)]
        public decimal Price { get; set; }

        [SugarColumn(DefaultValue = "0")]
        public bool IsFeatured { get; set; } = false;

        [SugarColumn(DefaultValue = "1")]
        public bool IsActive { get; set; } = true;

        [SugarColumn(DefaultValue = "0")]
        public int ViewsCount { get; set; } = 0;

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

        [SugarColumn(IsIgnore = true)]
        public List<ProductMedia>? MediaList { get; set; }

        
        [SugarColumn(IsIgnore = true)]
        public List<ProductSpec>? Specs { get; set; }
    }
}
