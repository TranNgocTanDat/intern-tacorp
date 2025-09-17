using beSQLSugar.Application.DTO.response;
using MediatR;

namespace beSQLSugar.Application.Queries
{
    public record GetAllAdminUsersQuery() : IRequest<List<AdminUserResponse>>;
}
