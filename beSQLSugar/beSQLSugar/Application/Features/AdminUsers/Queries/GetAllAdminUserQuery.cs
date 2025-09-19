using beSQLSugar.Application.DTO.response;
using MediatR;

namespace beSQLSugar.Application.Features.AdminUsers.Queries
{

    // query lấy tất cả AdminUser dùng Mediator pattern
    public class GetAllAdminUsersQuery : IRequest<List<AdminUserResponse>>
    {
        public GetAllAdminUsersQuery() { }
    }
}
