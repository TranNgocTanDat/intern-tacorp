using beSQLSugar.Application.DTOs.request;
using beSQLSugar.Application.DTOs.response;
using beSQLSugar.Application.Features.Login.Commands;
using beSQLSugar.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace beSQLSugar.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<APIResponse<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            try
            {
                var cmd = new LoginCommand(request);
                var resp = await _mediator.Send(cmd);
                return APIResponse<LoginResponse>.Success(resp);
            }
            catch (System.UnauthorizedAccessException ex)
            {
                return APIResponse<LoginResponse>.Unauthorized(ex.Message);
            }
        }


        [HttpGet("me")]
        [Authorize]
        public IActionResult Me()
        {
            return Ok(new { username = User.Identity?.Name });
        }
    }
}
