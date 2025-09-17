using SqlSugar;
namespace beSQLSugar.Models
{
    [SugarTable("analytics_visits")]
    public class AnalyticsVisit
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(IsNullable = true)]
        public int? ProductId { get; set; }

        [SugarColumn(Length = 200)]
        public string? SessionId { get; set; }

        [SugarColumn(Length = 50)]
        public string? IpAddress { get; set; }

        [SugarColumn(Length = 1000)]
        public string? UserAgent { get; set; }

        [SugarColumn(Length = 50)]
        public string? EventType { get; set; }  

        [SugarColumn(DefaultValue = "sysdatetime()")]
        public DateTime CreateTime { get; set; }
    }
}
