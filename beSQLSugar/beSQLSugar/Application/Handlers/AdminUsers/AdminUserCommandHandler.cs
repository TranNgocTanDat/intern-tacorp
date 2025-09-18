using beSQLSugar.Application.Commands.AdminUsers;
using beSQLSugar.Application.DTO.response;
using beSQLSugar.Application.ServiceInterfaces;
using MediatR;

namespace beSQLSugar.Application.Handlers.AdminUsers
{
    public class AdminUserCommandHandler : IRequestHandler<CreateAdminUserCommand, AdminUserResponse>,
        IRequestHandler<UpdateAdminUserCommand, AdminUserResponse?>,
        IRequestHandler<DeleteAdminUserCommand, bool>
    {
        private readonly IAdminUserService _service;

        public AdminUserCommandHandler(IAdminUserService service)
        {
            _service = service;
        }
        public async Task<AdminUserResponse> Handle(CreateAdminUserCommand request, CancellationToken cancellationToken)
        {
           return await _service.CreateAsync(request.Request);
        }

        public async Task<AdminUserResponse?> Handle(UpdateAdminUserCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateAsync(request.Id, request.Request);
        }

        public async Task<bool> Handle(DeleteAdminUserCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteAsync(request.Id);
        }
    }
}
