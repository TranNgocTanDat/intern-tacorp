using beSQLSugar.Application.Commands;
using beSQLSugar.Application.DTO.request;
using beSQLSugar.Application.DTO.response;
using beSQLSugar.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace beSQLSugar.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminUserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdminUserResponse>> GetById(int id)
        {
            var query = new GetAdminUserByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<AdminUserResponse>>> GetAll()
        {
            var query = new GetAllAdminUsersQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<AdminUserResponse>> Create([FromBody] AdminUserRequest request)
        {
            var command = new CreateAdminUserCommand(request);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AdminUserResponse>> Update(int id, [FromBody] AdminUserRequest request)
        {
            var command = new UpdateAdminUserCommand(id, request);
            var result = await _mediator.Send(command);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteAdminUserCommand(id);
            var success = await _mediator.Send(command);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
