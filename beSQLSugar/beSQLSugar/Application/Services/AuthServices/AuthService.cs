using beSQLSugar.Application.Dto.request.Auth;
using beSQLSugar.Application.Dto.response.Admin;
using beSQLSugar.Application.Dto.response.Auth;
using beSQLSugar.Application.Services.JWT;
using beSQLSugar.Infrastructure.Repositories.AdminRepository;

namespace beSQLSugar.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAdminUserRepository _adminUserRepository;
        private readonly IJWTService _jwtService;

        public AuthService(IAdminUserRepository adminUserRepository, IJWTService jwtService)
        {
            _adminUserRepository = adminUserRepository;
            _jwtService = jwtService;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
           var user = await _adminUserRepository.GetByUsernameAsync(request.Username);
            if(user == null || !user.IsActive)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            if (user.Role != "Admin")
                throw new UnauthorizedAccessException("Only admin can login");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            var token = _jwtService.GenerateToken(user);

            return new LoginResponse
            {
                AccessToken = token.Token,
                ExpiresIn = token.ExpiresInSeconds,
                AdminUser = new AdminUserResponse
                {
                    Id = user.Id,
                    Username = user.Username,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = user.Role
                }
            };
        }
    }
}
