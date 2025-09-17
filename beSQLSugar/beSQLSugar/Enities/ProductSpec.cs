using SqlSugar;
namespace beSQLSugar.Models
{
    [SugarTable("product_spec")]
    public class ProductSpec
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        public int ProductId { get; set; }

        [SugarColumn(Length = 200, IsNullable = false)]
        public string SpecKey { get; set; } = string.Empty;

        [SugarColumn(Length = 1000)]
        public string? SpecValue { get; set; }

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
