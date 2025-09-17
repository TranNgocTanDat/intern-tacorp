using AutoMapper;
using beSQLSugar.DTO.request;
using beSQLSugar.models;
using beSQLSugar.DTO.response;


namespace backendTA.Mapping
{
    public class DeviceProfile : Profile
    {
        public DeviceProfile()
        {
            // Add your mappings here
            CreateMap<Device, DeviceResponse>()
                .ForMember(dest => dest.DeviceTypeName, opt => opt.MapFrom(src => src.DeviceType != null ? src.DeviceType.Name : null));
            CreateMap<DeviceRequest, Device>();

            
            CreateMap<DeviceType, DeviceTypeResponse>();
            CreateMap<DeviceTypeRequest, DeviceType>();
        }
    }
}
