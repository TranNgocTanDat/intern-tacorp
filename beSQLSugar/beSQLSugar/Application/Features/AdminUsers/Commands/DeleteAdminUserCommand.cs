using MediatR;

namespace beSQLSugar.Application.Commands
{
    public record DeleteAdminUserCommand(int Id) : IRequest<bool>;
}
