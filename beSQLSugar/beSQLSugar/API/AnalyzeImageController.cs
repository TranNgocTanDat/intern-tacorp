using beSQLSugar.Application.Dto.request.AnalyzeImage;
using beSQLSugar.Application.Dto.response.AnalyzeImage;
using beSQLSugar.Application.Features.AnalyzeImage.Commands;
using beSQLSugar.Share.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace beSQLSugar.API
{
    [ApiController]
    [Route("api/analyzed-image")]
    public class AnalyzeImageController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AnalyzeImageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Tạo analyze image
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<APIResponse<AnalyzeImageResponse>> AddAsync([FromForm] AnalyzeImageRequest request)
        {
            var command = new CreateAnalyzeCommand(request);
            var result = await _mediator.Send(command);
            return APIResponse<AnalyzeImageResponse>.Success(result, "Tải ảnh lên thành công");
        }

        [HttpPost("find-path")]
        public async Task<APIResponse<FindPathResponse>> FindPath([FromBody] FindPathRequest request)
        {
            var command = new FindPathCommand(request);
            var result = await _mediator.Send(command);
            return APIResponse<FindPathResponse>.Success(result, "Tìm đường đi thành công");
        }
    }
}
