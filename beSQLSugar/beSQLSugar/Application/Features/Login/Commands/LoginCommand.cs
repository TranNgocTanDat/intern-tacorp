using beSQLSugar.Application.Dto.request.Auth;
using beSQLSugar.Application.Dto.response.Auth;
using MediatR;

namespace beSQLSugar.Application.Features.Login.Commands
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public LoginRequest Request { get; set; }
        public LoginCommand(LoginRequest request)
        {
            Request = request;
        }
    }
}
