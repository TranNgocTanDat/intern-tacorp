namespace beSQLSugar.Application.DTO.response
{
    public class AdminUserResponse
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}
