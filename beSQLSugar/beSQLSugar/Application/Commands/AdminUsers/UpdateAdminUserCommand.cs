using beSQLSugar.Application.DTO.request;
using beSQLSugar.Application.DTO.response;
using MediatR;

namespace beSQLSugar.Application.Commands.AdminUsers
{

    public record UpdateAdminUserCommand(int Id, AdminUserRequest Request) : IRequest<AdminUserResponse?>;
}
