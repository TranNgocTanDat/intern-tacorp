using SqlSugar;
namespace beSQLSugar.Domain.Enities
{
    [SugarTable("categories")]
    public class Category
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        [SugarColumn(Length = 150, IsNullable = false)]
        public string Name { get; set; } = string.Empty;

        [SugarColumn(Length = 150, IsNullable = false)]
        public string Slug { get; set; } = string.Empty;

        [SugarColumn(IsNullable = true)]
        public int? ParentId { get; set; }

        [SugarColumn(Length = 1000, IsNullable = true)]
        public string? Description { get; set; }

        [SugarColumn(DefaultValue = "0")]
        public int OrderIndex { get; set; } = 0;

        [SugarColumn(DefaultValue = "1")]
        public bool IsActive { get; set; } = true;

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
        public List<Category>? Children { get; set; }

        
        [SugarColumn(IsIgnore = true)]
        public List<Product>? Products { get; set; }

    }
}
