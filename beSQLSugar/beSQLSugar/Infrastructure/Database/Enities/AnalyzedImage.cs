using SqlSugar;

namespace beSQLSugar.Infrastructure.Database.Enities
{
    [SugarTable("analyzed_image")]
    public class AnalyzedImage
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        [SugarColumn(ColumnDataType = "NVARCHAR(256)")]
        public string? FilePathUrl { get; set; }
        [SugarColumn(ColumnDataType = "NVARCHAR(256)", IsNullable = true)]
        public string? FilePathMap { get; set; }
        [SugarColumn(DefaultValue = "0")]
        public bool IsMapLike { get; set; }
        [SugarColumn(IsNullable = true)]
        public DateTime CreatedTime { get; set; }
        // 🧩 Thêm cột mới để lưu chuỗi nén Base64
        [SugarColumn(IsNullable = true, ColumnDataType = "NVARCHAR(MAX)")]
        public string? GridDataCompressed { get; set; }

    }
}
