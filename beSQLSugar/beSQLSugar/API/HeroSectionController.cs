using beSQLSugar.Application.DTO.request;
using beSQLSugar.Application.DTO.response;
using beSQLSugar.Application.DTOs.request;
using beSQLSugar.Application.Features.HeroSection.Commands;
using beSQLSugar.Application.Features.HeroSection.Queries;
using beSQLSugar.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace beSQLSugar.API
{
    [ApiController]
    [Route("api/hero-section")]
    public class HeroSectionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public HeroSectionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Thêm mới HeroSection
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<APIResponse<HeroSectionResponse>> CreateHeroSection([FromForm] HeroSectionRequest request)
        {
            // Tạo command và gửi qua mediator
            var command = new CreateHeroSectionCommand(request);

            // Nhận kết quả từ handler
            var result = await _mediator.Send(command);

            // Trả về response
            return APIResponse<HeroSectionResponse>.Success(result, "Hero section đã tạo thành công");
        }

        // Update HeroSection
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<APIResponse<HeroSectionResponse?>> UpdateHeroSection(int id, [FromForm] HeroSectionRequest request)
        {
            // Tạo command và gửi qua mediator
            var command = new UpdateHeroSectionCommand(id, request);
            // Nhận kết quả từ handler
            var result = await _mediator.Send(command);
            // Kiểm tra kết quả và trả về response
            if (result == null)
            {
                return APIResponse<HeroSectionResponse?>.NotFound("Không tìm thấy Hero section với ID này.");
            }
            return APIResponse<HeroSectionResponse?>.Success(result, "Cập nhật Hero section thành công.");
        }

        // Xóa HeroSection
        [HttpDelete("{id}")]
        public async Task<APIResponse<bool>> DeleteHeroSection(int id)
        {
            // Tạo command và gửi qua mediator
            var command = new DeleteHeroSectionCommand(id);
            // Nhận kết quả từ handler
            var result = await _mediator.Send(command);
            // Kiểm tra kết quả và trả về response
            if (!result)
            {
                return APIResponse<bool>.NotFound("Không tìm thấy Hero section với ID này.");
            }
            return APIResponse<bool>.Success(result, "Xóa Hero section thành công.");
        }

        // Lấy HeroSection theo id
        [HttpGet("{id}")]
        public async Task<APIResponse<HeroSectionResponse?>> GetHeroSectionById(int id)
        {
            // Tạo query và gửi qua mediator
            var query = new GetHeroSectionByIdQuery(id);
            // Nhận kết quả từ handler
            var result = await _mediator.Send(query);
            // Kiểm tra và trả về response
            if (result == null)
            {
                return APIResponse<HeroSectionResponse?>.NotFound("Không tìm thấy Hero section với ID này.");
            }
            return APIResponse<HeroSectionResponse?>.Success(result, "Lấy dữ liệu thành công.");
        }

        // Lấy tất cả HeroSection
        [HttpGet]
        public async Task<APIResponse<List<HeroSectionResponse>>> GetAllHeroSection()
        {
            // Tạo query và gửi qua mediator
            var query = new GetAllHeroSectionQuery();

            // Nhận kết quả từ handler
            var result = await _mediator.Send(query);

            // Trả vể response
            return APIResponse<List<HeroSectionResponse>>.Success(result, "Lấy danh sách thành công");
        }

        // Filter HeroSection theo các tiêu chí trong request
        [HttpGet("filter")]
        public async Task<APIResponse<List<HeroSectionResponse>>> FilterHeroSections([FromQuery] HeroSectionFilterRequest request)
        {
            var query = new FilterHeroSectionQuery(request);
            var result = await _mediator.Send(query);
            return APIResponse<List<HeroSectionResponse>>.Success(result, "Lấy dữ liệu thành công.");
        }

    }
}
