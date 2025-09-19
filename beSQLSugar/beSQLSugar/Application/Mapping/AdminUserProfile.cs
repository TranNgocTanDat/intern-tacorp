using AutoMapper;
using beSQLSugar.Application.DTO.request;
using beSQLSugar.Application.DTO.response;
using beSQLSugar.Domain.Enities;

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
