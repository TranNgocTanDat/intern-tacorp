using beSQLSugar.Application.Commands.Login;
using beSQLSugar.Application.DTOs.request;
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
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var cmd = new LoginCommand(request);
                var resp = await _mediator.Send(cmd);
                return Ok(resp);
            }
            catch (System.UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
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
