using beSQLSugar.Application.DTO.response;
using beSQLSugar.Application.Features.AdminUsers.Queries;
using beSQLSugar.Application.ServiceInterfaces;
using MediatR;

namespace beSQLSugar.Application.Features.AdminUsers.Handlers
{
    //Xử lý query 
    public class AdminUserQueryHandler : IRequestHandler<GetAdminUserByIdQuery, AdminUserResponse?>,
        IRequestHandler<GetAllAdminUsersQuery, List<AdminUserResponse>>, IRequestHandler<SearchAdminUserQuery, List<AdminUserResponse>>
    {
        // Inject IAdminUserService để xử lý logic
        private readonly IAdminUserService _service;

        // khởi tạo
        public AdminUserQueryHandler(IAdminUserService service)
        {
            _service = service;
        }

        // xử lý lấy AdminUser theo id
        public async Task<AdminUserResponse?> Handle(GetAdminUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetByIdAsync(request.Id);
        }

        // xử lý lấy tất cả AdminUser
        public async Task<List<AdminUserResponse>> Handle(GetAllAdminUsersQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAllAsync();
        }

        // xử lý tìm kiếm AdminUser
        public async Task<List<AdminUserResponse>> Handle(SearchAdminUserQuery request, CancellationToken cancellationToken)
        {
            if (request.Request == null)
            {
                return new List<AdminUserResponse>();
            }

            var result = await _service.SearchAsync(request.Request);
            return result;
        }
    }
}
