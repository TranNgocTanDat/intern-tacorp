namespace beSQLSugar.Application.Dto.request.Contact
{
    public class ContactFilterRequest
    {
        public string Fullname { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? UserNote { get; set; }
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }

        public string? Status { get; set; }
        public int? HandleByAdminId { get; set; }
        
        public string? AdminNote { get; set; }
        public int? WriteIUid { get; set; }
        public DateTime? FromUpdateTime { get; set; }
        public DateTime? ToUpdateTime { get; set; }

    }
}
