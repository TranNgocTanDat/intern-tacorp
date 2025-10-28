namespace beSQLSugar.Application.Dto.request.Contact
{
    public class UpdateContactStatusRequest
    {
        public string? Status { get; set; }
        public int? HandleByAdminId { get; set; }
        public DateTime? HandleTime { get; set; }
        public string? AdminNote { get; set; }
        public int? WriteIUid { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
