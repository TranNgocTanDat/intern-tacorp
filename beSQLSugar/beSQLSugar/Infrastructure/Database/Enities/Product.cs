using SqlSugar;
namespace beSQLSugar.Infrastructure.Database.Enities
{
    [SugarTable("products")]
    public class Product
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(IsNullable = false)]
        public int CategoryId { get; set; } // liên kết với Category

        [SugarColumn(ColumnDataType = "nvarchar(250)", IsNullable = false)]
        public string? ProductName { get; set; }

        [SugarColumn(ColumnDataType = "nvarchar(250)", IsNullable = false)]
        public string? Slug { get; set; } 

        [SugarColumn(ColumnDataType = "nvarchar(250)")]
        public string? ShortDescription { get; set; }

        [SugarColumn(ColumnDataType = "nvarchar(max)")]
        public string? LongDescription { get; set; }

        [SugarColumn(IsNullable = true)]
        public decimal OriginalPrice { get; set; }
        [SugarColumn(IsNullable = true)]
        public decimal DiscountPrice { get; set; }
        [SugarColumn(IsNullable = true)]
        public int Discount { get; set; }
        [SugarColumn(DefaultValue = "0")]
        public bool IsFeatured { get; set; } = false;

        [SugarColumn(DefaultValue = "1")]
        public bool IsActive { get; set; } = true;

        [SugarColumn(DefaultValue = "0")]
        public int ViewsCount { get; set; } = 0;
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

        [SugarColumn(ColumnDataType = "nvarchar(250)", IsNullable = true)]
        public string? Note { get; set; }

        [SugarColumn(ColumnDataType = "nvarchar(120)", IsNullable = true)]
        public string? Option1 { get; set; }

        [SugarColumn(ColumnDataType = "nvarchar(120)", IsNullable = true)]
        public string? Option2 { get; set; }

        [SugarColumn(ColumnDataType = "nvarchar(120)", IsNullable = true)]
        public string? Option3 { get; set; }

        [SugarColumn(ColumnDataType = "nvarchar(120)", IsNullable = true)]
        public string? Option4 { get; set; }

        [SugarColumn(ColumnDataType = "nvarchar(120)", IsNullable = true)]
        public string? Option5 { get; set; }

        // 🔑 Navigation: 1 Product -> nhiều Media
        [Navigate(NavigateType.OneToMany, nameof(ProductMedia.ProductId))]
        public List<ProductMedia>? MediaList { get; set; }

        // 🔑 Navigation: 1 Product -> nhiều Specs
        [Navigate(NavigateType.OneToMany, nameof(ProductSpec.ProductId))]
        public List<ProductSpec>? Specs { get; set; }

        [Navigate(NavigateType.OneToOne, nameof(CategoryId))]
        public Category? Category { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(ProductColor.ProductId))]
        public List<ProductColor>? Colors { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(ProductStorage.ProductId))]
        public List<ProductStorage>? Storages { get; set; }
    }
}
