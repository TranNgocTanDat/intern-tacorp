namespace beSQLSugar.Application.Dto.request.Contact
{
    public class ContactRequest
    {
        public string Fullname { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? UserNote { get; set; }
        public int? ProductId { get; set; }
        
   
    }
}
