using MediatR;

namespace beSQLSugar.Application.Commands.AdminUsers
{
    public record DeleteAdminUserCommand(int Id) : IRequest<bool>;
}
