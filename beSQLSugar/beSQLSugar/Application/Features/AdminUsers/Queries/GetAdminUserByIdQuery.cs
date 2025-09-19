using beSQLSugar.Application.DTO.response;
using MediatR;

namespace beSQLSugar.Application.Features.AdminUsers.Queries
{
    // query lấy AdminUser theo ID dùng Mediator pattern
    public class GetAdminUserByIdQuery : IRequest<AdminUserResponse?>
    {
        public int Id { get; set; }

        public GetAdminUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
