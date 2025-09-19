using beSQLSugar.Application.DTOs.request;
using beSQLSugar.Application.DTOs.response;
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
