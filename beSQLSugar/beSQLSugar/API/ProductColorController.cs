using beSQLSugar.Application.Dto.request.ProductColor;
using beSQLSugar.Application.Dto.response.ProductColor;
using beSQLSugar.Application.Features.ProductColor.Commands;
using beSQLSugar.Application.Features.ProductColor.Queries;
using beSQLSugar.Share.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace beSQLSugar.API
{
    [ApiController]
    [Route("api/product-color")]
    public class ProductColorController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductColorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("product/{productId}")]
        public async Task<APIResponse<List<ProductColorResponse>>> GetColorsByProductId(int productId)
        {
            var query = new GetByProductIdQuery(productId);
            var result = await _mediator.Send(query);
            return APIResponse<List<ProductColorResponse>>.Success(result, "Lấy thành công");
        }

        [HttpGet("{colorId}/product/{productId}")]
        public async Task<APIResponse<List<ProductColorResponse>>> GetColorsByProductId(int productId, int colorId)
        {
            var query = new GetByProductIdAndColorIdQuery(productId, colorId);
            var result = await _mediator.Send(query);
            return APIResponse<List<ProductColorResponse>>.Success(result, "Lấy thành công");
        }

        // Lấy tất cả màu sắc
        [HttpGet]
        public async Task<APIResponse<List<ProductColorResponse>>> GetAllColors()
        {
            var query = new GetAllProducColorQuery();
            var result = await _mediator.Send(query);
            return APIResponse<List<ProductColorResponse>>.Success(result, "Lấy thành công");
        }

        // Update the CreateProductColor method to accept a ProductColorRequest parameter
        [HttpPost]
        public async Task<APIResponse<ProductColorResponse>> CreateProductColor([FromBody] ProductColorRequest request)
        {
            var command = new CreateProductColorCommand(request);
            var result = await _mediator.Send(command);
            return APIResponse<ProductColorResponse>.Success(result, "Tạo thành công");
        }

        [HttpPut("{id}")]
        public async Task<APIResponse<ProductColorResponse>> UpdateProductColor(int id, [FromBody] ProductColorRequest request)
        {
            var command = new UpdateProductColorCommand(id, request);
            var result = await _mediator.Send(command);
            return APIResponse<ProductColorResponse>.Success(result, "Cập nhật thành công");
        }
        [HttpDelete("{id}")]
        public async Task<APIResponse<bool>> DeleteProductColor(int id)
        {
            var command = new DeleteProductColorCommand(id);
            var result = await _mediator.Send(command);
            return APIResponse<bool>.Success(result, "Xóa thành công");
        }

        // Replace the FilterProductColors method with the following:

        [HttpGet("filter")]
        public async Task<APIResponse<List<ProductColorResponse>>> FilterProductColors([FromQuery] ProductColorFilterRequest request)
        {
            var query = new FilterProductColorQuery(request);
            var result = await _mediator.Send(query);
            return APIResponse<List<ProductColorResponse>>.Success(result, "Lọc thành công");
        }
    }
}
