using beSQLSugar.Application.DTO.request;
using beSQLSugar.Application.DTO.response;
using MediatR;

namespace beSQLSugar.Application.Commands
{
    
        public record CreateAdminUserCommand(AdminUserRequest Request) : IRequest<AdminUserResponse>;
    
}
