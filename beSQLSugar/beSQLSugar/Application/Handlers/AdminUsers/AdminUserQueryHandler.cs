using beSQLSugar.Application.DTO.response;
using beSQLSugar.Application.Queries.AdminUsers;
using beSQLSugar.Application.ServiceInterfaces;
using MediatR;

namespace beSQLSugar.Application.Handlers.AdminUsers
{
    public class AdminUserQueryHandler : IRequestHandler<GetAdminUserByIdQuery, AdminUserResponse?>,
        IRequestHandler<GetAllAdminUsersQuery, List<AdminUserResponse>>
    {
        private readonly IAdminUserService _service;

        public AdminUserQueryHandler(IAdminUserService service)
        {
            _service = service;
        }
        public async Task<AdminUserResponse?> Handle(GetAdminUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetByIdAsync(request.Id);
        }

        public async Task<List<AdminUserResponse>> Handle(GetAllAdminUsersQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAllAsync();
        }
    }
}
