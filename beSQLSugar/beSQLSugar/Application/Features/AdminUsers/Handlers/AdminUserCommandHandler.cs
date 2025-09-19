using beSQLSugar.Application.DTO.response;
using beSQLSugar.Application.Features.AdminUsers.Commands;
using beSQLSugar.Application.ServiceInterfaces;
using MediatR;

namespace beSQLSugar.Application.Features.AdminUsers.Handlers
{
    // command xử lý tạo, cập nhật, xóa AdminUser dùng Mediator pattern
    public class AdminUserCommandHandler : IRequestHandler<CreateAdminUserCommand, AdminUserResponse>,
        IRequestHandler<UpdateAdminUserCommand, AdminUserResponse?>,
        IRequestHandler<DeleteAdminUserCommand, bool>
    {
        // inject IAdminUserService để xử lý logic
        private readonly IAdminUserService _service;

        // khởi tạo 
        public AdminUserCommandHandler(IAdminUserService service)
        {
            _service = service;
        }

        // xử lý tạo mới AdminUser
        public async Task<AdminUserResponse> Handle(CreateAdminUserCommand request, CancellationToken cancellationToken)
        {
           return await _service.CreateAsync(request.Request);
        }

        // xử lý cập nhật AdminUser
        public async Task<AdminUserResponse?> Handle(UpdateAdminUserCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateAsync(request.Id, request.Request);
        }

        // xử lý xóa AdminUser
        public async Task<bool> Handle(DeleteAdminUserCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteAsync(request.Id);
        }
    }
}
