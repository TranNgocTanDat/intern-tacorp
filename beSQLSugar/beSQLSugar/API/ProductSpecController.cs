using beSQLSugar.Application.Dto.request.ProductSpec;
using beSQLSugar.Application.Dto.response.ProductSpec;
using beSQLSugar.Application.Features.ProductSpec.Commands;
using beSQLSugar.Application.Features.ProductSpec.Queries;
using beSQLSugar.Share.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace beSQLSugar.API
{
    [ApiController]
    [Route("api/spec")]
    public class ProductSpecController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductSpecController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Lấy ProductSpecs của sản phẩm
        [HttpGet("product/{productId}")]
        public async Task<APIResponse<List<ProductSpecResponse>>> GetProductSpecs(int productId)
        {
            var query = new GetSpecProductByIdQuery(productId);
            var result = await _mediator.Send(query);
            return APIResponse<List<ProductSpecResponse>>.Success(result, "Lấy specs của sản phẩm thành công");
        }
        
        [HttpGet]
        public async Task<APIResponse<List<ProductSpecResponse>>> GetAllProductSpecs()
        {
            var query = new GetAllSpecQuery();
            var result = await _mediator.Send(query);
            return APIResponse<List<ProductSpecResponse>>.Success(result, "Lấy tất cả specs của sản phẩm thành công");
        }
        
        [HttpGet("filter")]
        public async Task<APIResponse<List<ProductSpecResponse>>> FilterProductSpecs([FromQuery] ProductSpecFilterRequest request)
        {
            var query = new FilterSpecQuery(request);
            var result = await _mediator.Send(query);
            return APIResponse<List<ProductSpecResponse>>.Success(result, "Filter specs của sản phẩm thành công");
        }

        // Thêm ProductSpec cho sản phẩm
        [HttpPost("product/{productId}")]
        [Authorize(Roles = "Admin")]
        public async Task<APIResponse<ProductSpecResponse>> AddProductSpec(int productId, [FromBody] ProductSpecRequest request)
        {

            var command = new AddSpecCommand(productId, request);
            var result = await _mediator.Send(command);
            if (result == null)
            {
                return APIResponse<ProductSpecResponse>.BadRequest("Thêm spec thất bại");
            }
            return APIResponse<ProductSpecResponse>.Created(result, "Thêm spec thành công");
        }

        [HttpPut("{specId}/product/{productId}")]
        [Authorize(Roles = "Admin")]
        public async Task<APIResponse<ProductSpecResponse>> UpdateProductSpec(int productId, int specId, [FromBody] ProductSpecRequest request)
        {
            var command = new UpdateSpecCommand(productId, specId, request);
            var result = await _mediator.Send(command);
            if (result == null)
            {
                return APIResponse<ProductSpecResponse>.BadRequest("Cập nhật spec thất bại");
            }
            return APIResponse<ProductSpecResponse>.Success(result, "Cập nhật spec thành công");
        }

        // Xóa ProductSpec
        [HttpDelete("{specId}")]
        public async Task<APIResponse<bool>> DeleteProductSpec(int specId)
        {
            var command = new DeleteSpecCommand(specId);
            var result = await _mediator.Send(command);
            return APIResponse<bool>.Success(result, "Xóa spec thành công");
        }
    }
}
