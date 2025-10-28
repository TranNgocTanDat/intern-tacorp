using SqlSugar;
namespace beSQLSugar.Infrastructure.Database.Enities
{
    [SugarTable("hero_section")]
    public class HeroSection
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(ColumnDataType = "NVARCHAR(256)")]
        public string? Title { get; set; }

        [SugarColumn(ColumnDataType = "NVARCHAR(256)")]
        public string? Description { get; set; }

        [SugarColumn(ColumnDataType = "NVARCHAR(256)")]
        public string? PageHero { get; set; }

        [SugarColumn(ColumnDataType = "NVARCHAR(256)")]
        public string? HeroMediaUrl { get; set; }

        [SugarColumn(ColumnDataType = "NVARCHAR(20)")]
        public string? HeroMediaType { get; set; }

        [SugarColumn(DefaultValue = "0")]
        public bool IsPublished { get; set; }

        [SugarColumn(IsNullable = true)]
        public DateTime? PublishFrom { get; set; }

        [SugarColumn(IsNullable = true)]
        public DateTime? PublishTo { get; set; }

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

        // Quan hệ One-to-Many: HeroSection -> HeroSectionProducts
        [Navigate(NavigateType.OneToMany, nameof(HeroSectionProduct.HeroSectionId))]
        public List<HeroSectionProduct>? HeroProducts { get; set; } 
    }
}
