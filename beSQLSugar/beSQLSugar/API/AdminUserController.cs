using beSQLSugar.Application.DTO.request;
using beSQLSugar.Application.DTO.response;
using beSQLSugar.Application.DTOs.request;
using beSQLSugar.Application.Features.AdminUsers.Commands;
using beSQLSugar.Application.Features.AdminUsers.Queries;
using beSQLSugar.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace beSQLSugar.API
{
    [ApiController]
    [Route("api/admin-users")]
    [Authorize(Roles = "Admin")]
    
    public class AdminUserController : ControllerBase
    {
        // Inject IMediator để sử dụng Mediator pattern
        private readonly IMediator _mediator;

        // Khởi tạo với IMediator
        public AdminUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Lấy người dùng theo id
        [HttpGet("{id}")]
        public async Task<APIResponse<AdminUserResponse>> GetById(int id)
        {
            // Tạo query và gửi qua mediator
            var query = new GetAdminUserByIdQuery(id);
            // Nhận kết quả từ handler
            var result = await _mediator.Send(query);
            // kiểm tra và trả về response
            if (result == null)
            {
                return APIResponse<AdminUserResponse>.NotFound("Không tìm thấy người dùng với ID này.");
            }
            return APIResponse<AdminUserResponse>.Success(result, "Lấy dữ liệu thành công.");
        }

        // Lấy tất cả người dùng
        [HttpGet]
        public async Task<APIResponse<List<AdminUserResponse>>> GetAll()
        {
            // Tạo query và gửi qua mediator
            var query = new GetAllAdminUsersQuery();
            // Nhận kết quả từ handler
            var result = await _mediator.Send(query);
            // Trả về response
            return APIResponse<List<AdminUserResponse>>.Success(result, "Lấy danh sách thành công.");
        }

        [HttpPost]
        public async Task<APIResponse<AdminUserResponse>> Create([FromBody] AdminUserRequest request)
        {
            // Tạo command và gửi qua mediator
            var command = new CreateAdminUserCommand(request);
            // Nhận kết quả từ handler
            var result = await _mediator.Send(command);
            // 201 Created
            return APIResponse<AdminUserResponse>.Created(result, "Tạo người dùng thành công.");
        }

        [HttpPut("{id}")]
        public async Task<APIResponse<AdminUserResponse?>> Update(int id, [FromBody] AdminUserRequest request)
        {
            // Tạo command và gửi qua mediator
            var command = new UpdateAdminUserCommand(id, request);
            // Nhận kết quả từ handler
            var result = await _mediator.Send(command);
            if (result == null)
            {
                return APIResponse<AdminUserResponse?>.NotFound("Không tìm thấy người dùng để cập nhật.");
            }
            return APIResponse<AdminUserResponse?>.Success(result, "Cập nhật người dùng thành công.");
        }

        // Xóa người dùng
        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> Delete(int id)
        {
            // Tạo command và gửi qua mediator
            var command = new DeleteAdminUserCommand(id);
            // Nhận kết quả từ handler
            var success = await _mediator.Send(command);
            if (!success)
            {
                return NotFound("Không tìm thấy người dùng để xóa.");
            }
            // 204 No Content không có data, trả về message thôi
            return Ok("Xóa người dùng thành công.");
        }

        // Tìm kiếm admin
        [HttpGet("search")]
        public async Task<APIResponse<List<AdminUserResponse>>> Search([FromQuery] AdminUserSearchRequest request)
        {
            // Tạo query và gửi qua mediator
            var query = new SearchAdminUserQuery(request);
            // Nhận kết quả từ handler
            var result = await _mediator.Send(query);
            // Trả về response
            return APIResponse<List<AdminUserResponse>>.Success(result, "Tìm kiếm thành công.");
        }
    }
}
