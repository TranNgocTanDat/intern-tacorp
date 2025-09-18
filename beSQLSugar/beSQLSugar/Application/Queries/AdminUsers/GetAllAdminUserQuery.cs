using beSQLSugar.Application.DTO.response;
using MediatR;

namespace beSQLSugar.Application.Queries.AdminUsers
{
    public record GetAllAdminUsersQuery() : IRequest<List<AdminUserResponse>>;
}
