using beSQLSugar.Application.DTO.request;
using beSQLSugar.Application.DTO.response;
using MediatR;

namespace beSQLSugar.Application.Features.AdminUsers.Commands
{
    // command cập nhật AdminUser theo ID dùng Mediator pattern
    public class UpdateAdminUserCommand : IRequest<AdminUserResponse?>
    {
        public int Id { get; set; }
        public AdminUserRequest Request { get; set; }

        public UpdateAdminUserCommand(int id, AdminUserRequest request)
        {
            Id = id;
            Request = request;
        }
    }
}
