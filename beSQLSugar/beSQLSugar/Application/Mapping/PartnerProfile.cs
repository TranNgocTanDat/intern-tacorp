using AutoMapper;
using beSQLSugar.Application.Dto.request.Partner;
using beSQLSugar.Application.Dto.response.Partner;
using beSQLSugar.Infrastructure.Database.Enities;

namespace beSQLSugar.Application.Mapping
{
    public class PartnerProfile : Profile
    {
        public PartnerProfile()
        {
            CreateMap<Partner, PartnerResponse>();

            CreateMap<PartnerRequest, Partner>()
                .ForMember(dest => dest.LogoUrl, opt => opt.Ignore())
            .ForMember(dest => dest.ImgDefaultUrl, opt => opt.Ignore())
            .ForMember(dest => dest.ImgDefaultUrl, opt => opt.Ignore());
        }
    }
}
