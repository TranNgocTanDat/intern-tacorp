namespace beSQLSugar.Application.DTO.request
{
    public class AnalyticsVisitRequest
    {
        public int? ProductId { get; set; }
        public string? SessionId { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? EventType { get; set; } 
    }
}
