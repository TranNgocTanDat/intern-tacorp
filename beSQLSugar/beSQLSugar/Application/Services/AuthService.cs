using beSQLSugar.Application.DTO.response;
using beSQLSugar.Application.DTOs.request;
using beSQLSugar.Application.DTOs.response;
using beSQLSugar.Application.ServiceInterfaces;
using beSQLSugar.Domain.RepositoryInterfaces;

namespace beSQLSugar.Application.Services
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

            if (!string.Equals(user.Username, "admin", StringComparison.OrdinalIgnoreCase))
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
                    Email = user.Email
                }
            };
        }
    }
}
