using MediatR;

namespace beSQLSugar.Application.Features.AdminUsers.Commands
{
    // command xóa AdminUser dùng Mediator pattern
    public class DeleteAdminUserCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteAdminUserCommand(int id)
        {
            Id = id;
        }
    }
}
