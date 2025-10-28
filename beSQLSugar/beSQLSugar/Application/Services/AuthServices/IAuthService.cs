using beSQLSugar.Application.Dto.request.Auth;
using beSQLSugar.Application.Dto.response.Auth;

namespace beSQLSugar.Application.Services.Auth
{
    public interface IAuthService 
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
