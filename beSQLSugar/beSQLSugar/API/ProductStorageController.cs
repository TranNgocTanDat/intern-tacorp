using beSQLSugar.Application.Dto.request.ProductStorage;
using beSQLSugar.Application.Dto.response.ProductColor;
using beSQLSugar.Application.Dto.response.ProductStorage;
using beSQLSugar.Application.Features.ProductStorage.Commands;
using beSQLSugar.Application.Features.ProductStorage.Queries;
using beSQLSugar.Share.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace beSQLSugar.API
{
    [ApiController]
    [Route("api/product-storage")]
    public class ProductStorageController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductStorageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("product/{productId}")]
        public async Task<APIResponse<List<ProductStorageResponse>>> GetColorsByProductId(int productId)
        {
            var query = new GetByProductIdQuery(productId);
            var result = await _mediator.Send(query);
            return APIResponse<List<ProductStorageResponse>>.Success(result, "Lấy thành công");
        }

        // Lấy danh sách với sản phẩm
        [HttpGet]
        public async Task<APIResponse<List<ProductStorageResponse>>> GetListWithProduct()
        {
            var query = new GetAllProductStorageQuery();
            var result = await _mediator.Send(query);
            return APIResponse<List<ProductStorageResponse>>.Success(result, "Lấy thành công");
        }

        // Lọc
        [HttpGet("filter")]
        public async Task<APIResponse<List<ProductStorageResponse>>> Filter([FromQuery] ProductStorageFilterRequest request)
        {
            var query = new FilterProductStorageQuery(request);
            var result = await _mediator.Send(query);
            return APIResponse<List<ProductStorageResponse>>.Success(result, "Lọc thành công");
        }

        // Tạo
        [HttpPost]
        public async Task<APIResponse<ProductStorageResponse>> Create([FromBody] ProductStorageRequest request)
        {
            var command = new CreateProductStorageCommand(request);
            var result = await _mediator.Send(command);
            return APIResponse<ProductStorageResponse>.Success(result, "Tạo mới thành công");
        }

        // Cập nhật
        [HttpPut("{id}")]
        public async Task<APIResponse<ProductStorageResponse>> Update(int id, [FromBody] ProductStorageRequest request)
        {
            var command = new UpdateProductStorageCommand(id, request);
            var result = await _mediator.Send(command);
            return APIResponse<ProductStorageResponse>.Success(result, "Cập nhật thành công");
        }

        // Xóa
        [HttpDelete("{id}")]
        public async Task<APIResponse<bool>> Delete(int id)
        {
            var command = new DeleteProductStorageCommand(id);
            var result = await _mediator.Send(command);
            return APIResponse<bool>.Success(result, "Xóa thành công");
        }
    }
}
