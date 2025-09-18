using beSQLSugar.Application.DTOs.request;
using beSQLSugar.Application.DTOs.response;

namespace beSQLSugar.Application.ServiceInterfaces
{
    public interface IAuthService 
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
