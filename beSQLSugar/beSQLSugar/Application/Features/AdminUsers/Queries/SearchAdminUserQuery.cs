using beSQLSugar.Application.Dto.request.Admin;
using beSQLSugar.Application.Dto.response.Admin;
using MediatR;

namespace beSQLSugar.Application.Features.AdminUsers.Queries
{
    public class SearchAdminUserQuery : IRequest<List<AdminUserResponse>>
    {
        public AdminUserSearchRequest? Request { get; set; }
        public SearchAdminUserQuery(AdminUserSearchRequest? request)
        {
            Request = request;
        }
    }
}
