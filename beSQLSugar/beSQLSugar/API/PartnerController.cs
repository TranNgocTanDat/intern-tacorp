using beSQLSugar.Application.Dto.request.Partner;
using beSQLSugar.Application.Dto.response.Partner;
using beSQLSugar.Application.Features.Partner.Commands;
using beSQLSugar.Application.Features.Partner.Queries;
using beSQLSugar.Share.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace beSQLSugar.API
{
    [ApiController]
    [Route("api/partners")]
    public class PartnerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PartnerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Thêm mới Partner
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<APIResponse<PartnerResponse>> CreatePartner([FromForm] PartnerRequest request)
        {
            // Tạo command và gửi qua mediator
            var command = new CreatePartnerComand(request);
            // Nhận kết quả từ handler
            var result = await _mediator.Send(command);
            // Trả về response
            return APIResponse<PartnerResponse>.Success(result, "Partner đã tạo thành công");
        }

        // Chỉnh sửa Partner
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<APIResponse<PartnerResponse>> UpdatePartner(int id, [FromForm] PartnerRequest request)
        {
            // Tạo command và gửi qua mediator
            var command = new UpdatePartnerCommand(id, request);
            // Nhận kết quả từ handler
            var result = await _mediator.Send(command);
            // Kiểm tra kết quả và trả về response
            if (result == null)
            {
                return APIResponse<PartnerResponse>.NotFound("Không tìm thấy Partner với ID này.");
            }
            return APIResponse<PartnerResponse>.Success(result, "Cập nhật Partner thành công.");
        }

        // Xóa Partner
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<APIResponse<bool>> DeletePartner(int id)
        {
            // Tạo command và gửi qua mediator
            var command = new DeletePartnerCommand(id);
            // Nhận kết quả từ handler
            var result = await _mediator.Send(command);
            // Kiểm tra kết quả và trả về response
            if (!result)
            {
                return APIResponse<bool>.NotFound("Không tìm thấy Partner với ID này.");
            }
            return APIResponse<bool>.Success(result, "Xóa Partner thành công.");
        }

        // Lấy tất cả Partner
        [HttpGet]
        public async Task<APIResponse<List<PartnerResponse>>> GetAllPartners()
        {
            // Tạo query và gửi qua mediator
            var query = new GetAllPartnerQuery();
            // Nhận kết quả từ handler
            var result = await _mediator.Send(query);
            // Trả về response
            return APIResponse<List<PartnerResponse>>.Success(result, "Lấy danh sách Partner thành công.");
        }

        // Lấy Partner theo ID
        [HttpGet("{id}")]
        public async Task<APIResponse<PartnerResponse>> GetPartnerById(int id)
        {
            // Tạo query và gửi qua mediator
            var query = new GetPartnerById(id);
            // Nhận kết quả từ handler
            var result = await _mediator.Send(query);
            // Kiểm tra kết quả và trả về response
            if (result == null)
            {
                return APIResponse<PartnerResponse>.NotFound("Không tìm thấy Partner với ID này.");
            }
            return APIResponse<PartnerResponse>.Success(result, "Lấy Partner thành công.");
        }

        // Lọc Partner
        [HttpGet("filter")]
        public async Task<APIResponse<List<PartnerResponse>>> FilterPartners([FromQuery] PartnerFilterRequest request)
        {
            // Tạo query và gửi qua mediator
            var query = new FilterPartnerQuery(request);
            // Nhận kết quả từ handler
            var result = await _mediator.Send(query);
            // Trả về response
            return APIResponse<List<PartnerResponse>>.Success(result, "Lọc Partner thành công.");
        }
    }
}
