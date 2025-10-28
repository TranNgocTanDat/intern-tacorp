using beSQLSugar.Application.Dto.response.Admin;
using MediatR;

namespace beSQLSugar.Application.Features.AdminUsers.Queries
{

    // query lấy tất cả AdminUser dùng Mediator pattern
    public class GetAllAdminUsersQuery : IRequest<List<AdminUserResponse>>
    {
        public GetAllAdminUsersQuery() { }
    }
}
