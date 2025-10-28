using beSQLSugar.Application.Dto.request.ProductMedia;
using beSQLSugar.Application.Dto.response.ProductMedia;
using beSQLSugar.Application.Features.ProductMedia.Commands;
using beSQLSugar.Application.Features.ProductMedia.Queries;
using beSQLSugar.Share.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace beSQLSugar.API
{
    [ApiController]
    [Route("api/media")]
    public class ProductMediaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductMediaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<APIResponse<List<ProductMediaResponse>>> GetAllMedia()
        {
            var query = new GetAllMediaProductQuery();
            var result = await _mediator.Send(query);
            return APIResponse<List<ProductMediaResponse>>.Success(result);
        }

        [HttpGet("product/{productId}")]
        public async Task<APIResponse<List<ProductMediaResponse>>> GetMedia(int productId)
        {
            var query = new GetMediaProductByIdQuery(productId);
            var result = await _mediator.Send(query);
            return APIResponse<List<ProductMediaResponse>>.Success(result);
        }

        [HttpPost("product/{productId}")]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "Admin")]
        public async Task<APIResponse<ProductMediaResponse>> AddMedia(int productId, [FromForm] ProductMediaRequest request)
        {
            var command = new AddMediaCommand(productId, request);
            var result = await _mediator.Send(command);
            if (result == null) return APIResponse<ProductMediaResponse>.BadRequest("Thêm thất bại");
            return APIResponse<ProductMediaResponse>.Created(result, "Thêm thành công");
        }

        [HttpPut("{mediaId}/product/{productId}")]
        [Authorize(Roles = "Admin")]
        public async Task<APIResponse<ProductMediaResponse>> UpdateMedia(int productId, int mediaId, [FromForm] ProductMediaRequest request)
        {
            var command = new UpdateMediaCommand(productId, mediaId, request);
            var result = await _mediator.Send(command);
            if (result == null) return APIResponse<ProductMediaResponse>.BadRequest("Cập nhật thất bại");
            return APIResponse<ProductMediaResponse>.Success(result, "Cập nhật thành công");
        }

        [HttpGet("filter")]
        public async Task<APIResponse<List<ProductMediaResponse>>> FilterMedia([FromQuery] ProductMediaFilterRequest request)
        {
            var query = new FilterProductMediaQuery(request);
            var result = await _mediator.Send(query);
            return APIResponse<List<ProductMediaResponse>>.Success(result);
        }

        [HttpDelete("{mediaId}")]
        public async Task<APIResponse<bool>> DeleteMedia(int mediaId)
        {
            var command = new DeleteMediaCommand(mediaId);
            var result = await _mediator.Send(command);
            return APIResponse<bool>.Success(result, "Xoá thành công");
        }
    }
}
