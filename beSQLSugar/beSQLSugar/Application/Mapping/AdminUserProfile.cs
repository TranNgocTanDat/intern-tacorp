using AutoMapper;
using beSQLSugar.Application.DTO.request;
using beSQLSugar.Application.DTO.response;
using beSQLSugar.Domain.Enities;

namespace beSQLSugar.Application.Mapping
{
    public class AdminUserProfile : Profile
    {
        public AdminUserProfile()
        {
            CreateMap<AdminUser, AdminUserResponse>();

            CreateMap<AdminUserRequest, AdminUser>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
        }

    }
}
