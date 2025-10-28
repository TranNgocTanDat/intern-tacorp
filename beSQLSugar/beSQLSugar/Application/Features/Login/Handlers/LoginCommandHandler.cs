using beSQLSugar.Application.Dto.response.Auth;
using beSQLSugar.Application.Features.Login.Commands;
using beSQLSugar.Application.Services.Auth;
using MediatR;

namespace beSQLSugar.Application.Features.Login.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IAuthService _authService;
        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _authService.LoginAsync(request.Request);
        }
    }
    
}
