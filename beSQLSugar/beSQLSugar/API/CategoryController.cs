using beSQLSugar.Application.DTO.request;
using beSQLSugar.Application.DTO.response;
using beSQLSugar.Application.Features.Category.Commands;
using beSQLSugar.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using beSQLSugar.Application.Features.Category.Queries;
using beSQLSugar.Application.DTOs.request;

namespace beSQLSugar.API
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Thêm mới Category
        [HttpPost]
        public async Task<APIResponse<CategoryResponse>> CreateCategory([FromBody] CategoryRequest request)
        {
            // Tạo command và gửi qua mediator
            var command = new CreateCategoryCommand(request);
            // Nhận kết quả từ handler
            var result = await _mediator.Send(command);
            // Trả về response
            return APIResponse<CategoryResponse>.Success(result, "Category đã tạo thành công");
        }

        // Xóa Category
        [HttpDelete("{id}")]
        public async Task<APIResponse<bool>> DeleteCategory(int id)
        {
            // Tạo command và gửi qua mediator
            var command = new DeleteCategoryCommand(id);
            // Nhận kết quả từ handler
            var result = await _mediator.Send(command);
            // Kiểm tra kết quả và trả về response
            if (!result)
            {
                return APIResponse<bool>.NotFound("Không tìm thấy Category với ID này.");
            }
            return APIResponse<bool>.Success(result, "Xóa Category thành công.");
        }

        // Lấy tất cả Category
        [HttpGet]
        public async Task<APIResponse<List<CategoryResponse>>> GetAllCategories()
        {
            // Tạo query và gửi qua mediator
            var query = new GetAllCategoryQuery();
            
            // Nhận kết quả từ handler
            var result = await _mediator.Send(query);
            // Trả về response
            return APIResponse<List<CategoryResponse>>.Success(result, "Lấy danh sách thành công.");
        }

        // Lấy Category theo id
        [HttpGet("{id}")]
        public async Task<APIResponse<CategoryResponse?>> GetCategoryById(int id)
        {
            // Tạo query và gửi qua mediator
            var query = new GetCategoryByIdQuery(id);
            // Nhận kết quả từ handler
            var result = await _mediator.Send(query);
            // kiểm tra và trả về response
            if (result == null)
            {
                return APIResponse<CategoryResponse?>.NotFound("Không tìm thấy Category với ID này.");
            }
            return APIResponse<CategoryResponse?>.Success(result, "Lấy dữ liệu thành công.");
        }

        // Cập nhật Category
        [HttpPut("{id}")]
        public async Task<APIResponse<CategoryResponse?>> UpdateCategory(int id, [FromBody] CategoryRequest request)
        {
            // Tạo command và gửi qua mediator
            var command = new UpdateCategoryCommand(id, request);
            // Nhận kết quả từ handler
            var result = await _mediator.Send(command);
            // Kiểm tra kết quả và trả về response
            if (result == null)
            {
                return APIResponse<CategoryResponse?>.NotFound("Không tìm thấy Category với ID này.");
            }
            return APIResponse<CategoryResponse?>.Success(result, "Cập nhật Category thành công.");
        }

        // Filter Category
        [HttpGet("filter")]
        public async Task<APIResponse<List<CategoryResponse>>> FilterCategories([FromQuery] CategoryFilterRequest request)
        {
            // Tạo query và gửi qua mediator
            var query = new FilterCategoryQuery(request);
            // Nhận kết quả từ handler
            var result = await _mediator.Send(query);
            // Trả về response
            return APIResponse<List<CategoryResponse>>.Success(result, "Lọc danh sách thành công.");
        }

    }
}
