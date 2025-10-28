using beSQLSugar.Application.Dto.request.Admin;
using beSQLSugar.Application.Dto.response.Admin;
using MediatR;

namespace beSQLSugar.Application.Features.AdminUsers.Commands
{
    // command tạo mới AdminUser dùng Mediator pattern
    public class CreateAdminUserCommand : IRequest<AdminUserResponse>
    {
        // request chứa thông tin để tạo mới AdminUser
        public AdminUserRequest Request { get; set; }

        // khởi tạo với request
        public CreateAdminUserCommand(AdminUserRequest request)
        {
            Request = request;
        }
    }

}
