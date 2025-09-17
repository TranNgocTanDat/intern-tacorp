namespace beSQLSugar.DTO.response
{
    public class AnalyticsVisitResponse
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string? SessionId { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? EventType { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
