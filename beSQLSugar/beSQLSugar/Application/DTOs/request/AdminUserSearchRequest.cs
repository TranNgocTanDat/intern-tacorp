namespace beSQLSugar.Application.DTOs.request
{
    public class AdminUserSearchRequest
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public bool? IsActive { get; set; }
    }
}
