using SqlSugar;
namespace beSQLSugar.Infrastructure.Database.Enities
{
    [SugarTable("hero_section_product")]
    public class HeroSectionProduct
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(IsNullable = false)]
        public int HeroSectionId { get; set; }

        [SugarColumn(IsNullable = false)]
        public int ProductId { get; set; }

        [SugarColumn(DefaultValue = "0")]
        public int OrderIndex { get; set; }

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

        // Quan hệ nhiều HeroSectionProduct -> 1 HeroSection
        [Navigate(NavigateType.ManyToOne, nameof(HeroSectionId))]
        public HeroSection? HeroSection { get; set; }

        // Quan hệ nhiều HeroSectionProduct -> 1 Product
        [Navigate(NavigateType.ManyToOne, nameof(ProductId))]
        public Product? Product { get; set; }
    }
}
