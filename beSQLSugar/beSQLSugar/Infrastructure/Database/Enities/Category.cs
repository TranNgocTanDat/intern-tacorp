using SqlSugar;
namespace beSQLSugar.Infrastructure.Database.Enities
{
    [SugarTable("categories")]
    public class Category
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        [SugarColumn(ColumnDataType = "nvarchar(250)")]
        public string? Name { get; set; } 

        [SugarColumn(ColumnDataType = "nvarchar(250)")]
        public string? Slug { get; set; } 

        [SugarColumn(IsNullable = true)]
        public int? ParentId { get; set; }

        [SugarColumn(ColumnDataType = "nvarchar(1000)", IsNullable = true)]
        public string? Description { get; set; }

        [SugarColumn(DefaultValue = "0")]
        public int OrderIndex { get; set; } = 0;

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

        [SugarColumn(ColumnDataType = "nvarchar(120)", IsNullable = true)]
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

        // Liên kết với bảng Partner (hãng)
        [SugarColumn(IsNullable = true)]
        public int? PartnerId { get; set; }

        [Navigate(NavigateType.OneToOne, nameof(PartnerId))]
        public Partner? Partner { get; set; }

        // Quan hệ 1 Category -> nhiều Category con
        [Navigate(NavigateType.OneToMany, nameof(ParentId), nameof(Id))]
        public List<Category> Children { get; set; } = new();

        // Quan hệ Nhiều Category con -> 1 Category cha
        [Navigate(NavigateType.OneToOne, nameof(ParentId))]
        public Category? Parent { get; set; }

        // Quan hệ 1 Category -> nhiều Product
        [Navigate(NavigateType.OneToMany, nameof(Product.CategoryId))]
        public List<Product> Products { get; set; } = new();

    }
}
