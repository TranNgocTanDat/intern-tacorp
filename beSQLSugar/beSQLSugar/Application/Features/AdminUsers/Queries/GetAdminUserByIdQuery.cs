using beSQLSugar.Application.DTO.response;
using MediatR;

namespace beSQLSugar.Application.Queries
{
    public record GetAdminUserByIdQuery(int Id) : IRequest<AdminUserResponse?>;
}
