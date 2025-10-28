using AutoMapper;
using beSQLSugar.Application.Dto.request.Admin;
using beSQLSugar.Application.Dto.response.Admin;
using beSQLSugar.Infrastructure.Database.Enities;

namespace beSQLSugar.Application.Mapping
{
    public class AdminUserProfile : Profile
    {
        // Định nghĩa ánh xạ giữa AdminUser và các DTO liên quan
        public AdminUserProfile()
        {
            // Map từ AdminUser sang AdminUserResponse
            CreateMap<AdminUser, AdminUserResponse>();

            // Map từ AdminUserRequest sang AdminUser, bỏ qua PasswordHash 
            CreateMap<AdminUserRequest, AdminUser>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
        }

    }
}
