using beSQLSugar.Application.DTO.response;
using beSQLSugar.Application.DTOs.request;
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
