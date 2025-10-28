using AutoMapper;
using beSQLSugar.Application.Dto.request.AnalyzeImage;
using beSQLSugar.Application.Dto.response.AnalyzeImage;
using beSQLSugar.Infrastructure.Database.Enities;

namespace beSQLSugar.Application.Mapping
{
    public class AnalyzeImageProfile : Profile
    {
        public AnalyzeImageProfile()
        {
            CreateMap<AnalyzedImage, AnalyzeImageResponse>();
            CreateMap<AnalyzeImageRequest, AnalyzedImage>()
                .ForMember(dest => dest.FilePathUrl,
                otp => otp.Condition(src => src.FilePathUrl != null));
        }
    }
}
