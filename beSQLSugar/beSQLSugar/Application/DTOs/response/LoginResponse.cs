using beSQLSugar.Application.DTO.response;

namespace beSQLSugar.Application.DTOs.response
{
    public class LoginResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string TokenType { get; set; } = "Bearer";
        public long ExpiresIn { get; set; } // seconds

        public required AdminUserResponse AdminUser { get; set; }

    }
}
