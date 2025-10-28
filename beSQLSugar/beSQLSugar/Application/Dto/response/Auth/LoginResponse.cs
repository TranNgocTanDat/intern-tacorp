using beSQLSugar.Application.Dto.response.Admin;

namespace beSQLSugar.Application.Dto.response.Auth
{
    public class LoginResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string TokenType { get; set; } = "Bearer";
        public long ExpiresIn { get; set; } // seconds

        public required AdminUserResponse AdminUser { get; set; }

    }
}
