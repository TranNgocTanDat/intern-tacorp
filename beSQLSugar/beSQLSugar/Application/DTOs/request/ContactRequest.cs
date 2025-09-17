namespace beSQLSugar.Application.DTO.request
{
    public class ContactRequest
    {
        public string Fullname { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? UserNote { get; set; }
        public int? ProductId { get; set; }
        public string? Status { get; set; }
        public int? HandleByAdminId { get; set; }
        public DateTime? HandleTime { get; set; }
        public string? AdminNote { get; set; }
        public int? WriteIUid { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? Option1 { get; set; }
        public string? Option2 { get; set; }
        public string? Option3 { get; set; }
        public string? Option4 { get; set; }
        public string? Option5 { get; set; }
    }
}
