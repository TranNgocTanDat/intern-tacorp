using beSQLSugar.Application.Dto.request.Product;
using beSQLSugar.Application.Dto.response.Product;
using beSQLSugar.Application.Features.Product.Commands;
using beSQLSugar.Application.Features.Product.Queries;
using beSQLSugar.Application.Features.ProductMedia.Commands;
using beSQLSugar.Application.Features.ProductMedia.Queries;
using beSQLSugar.Application.Features.ProductSpec.Commands;
using beSQLSugar.Application.Features.ProductSpec.Queries;
using beSQLSugar.Share.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace beSQLSugar.API
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Thêm sản phẩm mới
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<APIResponse<ProductResponse>> CreateProduct([FromBody] ProductRequest request)
        {
            // Tạo command từ request
            var command = new CreateProductCommand(request);
            // Gửi command đến MediatR để xử lý
            var result = await _mediator.Send(command);
            return  APIResponse<ProductResponse>.Created(result, "Tạo sản phẩm thành công");

        }


        //Update sản phẩm
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<APIResponse<ProductResponse>> UpdateProduct(int id, [FromBody] ProductRequest request)
        {
            var command = new UpdateProductCommand(id, request);
            var result = await _mediator.Send(command);
            return APIResponse<ProductResponse>.Success(result, "Cập nhật sản phẩm thành công");
        }

        // Xóa sản phẩm
        [HttpDelete("{id}")]
        public async Task<APIResponse<bool>> DeleteProduct(int id)
        {
            var command = new DeleteProductCommand(id);
            var result = await _mediator.Send(command);
            return APIResponse<bool>.Success(result, "Xóa sản phẩm thành công");
        }

        // Lọc sản phẩm theo các tiêu chí trong request
        [HttpGet("filter")]
        public async Task<APIResponse<List<ProductResponse>>> FilterProducts([FromQuery] ProductFilterRequest filterRequest)
        {
            var query = new FilterProductQuery(filterRequest);
            var result = await _mediator.Send(query);
            return APIResponse<List<ProductResponse>>.Success(result, "Lọc sản phẩm thành công");
        }

        // Lấy sản phẩm theo slug
        [HttpGet("slug/{slug}")]
        public async Task<APIResponse<ProductResponse>> GetProductBySlug(string slug)
        {
            var query = new GetProductBySlugQuery(slug);
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return APIResponse<ProductResponse>.NotFound("Không tìm thấy sản phẩm");
            }
            return APIResponse<ProductResponse>.Success(result, "Lấy sản phẩm thành công");
        }

        // Lấy Featured products
        [HttpGet("featured")]
        public async Task<APIResponse<List<ProductResponse>>> GetFeaturedProducts()
        {
            var query = new GetFeatureProductsQuery();
            var result = await _mediator.Send(query);
            return APIResponse<List<ProductResponse>>.Success(result, "Lấy featured products thành công");
        }

        // Lấy chi tiết sản phẩm
        [HttpGet("{id}/details")]
        public async Task<APIResponse<ProductResponse>> GetProductWithDetails(int id)
        {
            var query = new GetProductWithDetailsQuery(id);
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return APIResponse<ProductResponse>.NotFound("Không tìm thấy sản phẩm");
            }
            return APIResponse<ProductResponse>.Success(result, "Lấy chi tiết sản phẩm thành công");
        }

        
    }
}
