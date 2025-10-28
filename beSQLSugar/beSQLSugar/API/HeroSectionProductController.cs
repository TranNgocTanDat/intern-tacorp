using beSQLSugar.Application.Dto.request.HeroSectionProduct;
using beSQLSugar.Application.Dto.response.HeroSectionProduct;
using beSQLSugar.Application.Features.HeroSectionProduct.Commands;
using beSQLSugar.Application.Features.HeroSectionProduct.Queries;
using beSQLSugar.Share.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace beSQLSugar.API
{
    [ApiController]
    [Route("api/hero-section-products")]
    public class HeroSectionProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HeroSectionProductController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<APIResponse<HeroSectionProductResponse?>> Create([FromBody] HeroSectionProductRequest request)
        {
            var command = new CreateHeroSectionProductCommand(request, User);
            var result = await _mediator.Send(command);
            return APIResponse<HeroSectionProductResponse?>.Success(result, "Hero section product created successfully");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<APIResponse<HeroSectionProductResponse>> Update(int id, [FromBody] HeroSectionProductRequest request)
        {
            var command = new UpdateHeroSectionProductCommand(id, request, User);
            var result = await _mediator.Send(command);
            return APIResponse<HeroSectionProductResponse>.Success(result, "Hero section product updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<APIResponse<bool>> Delete(int id)
        {
            var command = new DeleteHeroSectionProductCommand(id);
            var result = await _mediator.Send(command);
            if (!result)
            {
                return APIResponse<bool>.NotFound("Hero section product not found.");
            }
            return APIResponse<bool>.Success(true, "Hero section product deleted successfully.");
        }

        [HttpGet("herosection/{heroSectionId}")]
        public async Task<APIResponse<List<HeroSectionProductResponse>>> GetByHeroSectionId(int heroSectionId)
        {
            var query = new GetByHeroSectionIdQuery(heroSectionId);
            var result = await _mediator.Send(query);
            return APIResponse<List<HeroSectionProductResponse>>.Success(result, "Fetched hero section products successfully");
        }

        [HttpGet("herosection/{heroSectionId}/product/{productId}")]
        public async Task<ActionResult<HeroSectionProductResponse?>> GetByHeroSectionAndProduct(int heroSectionId, int productId)
        {
            var query = new GetByHeroSectionAndProductQuery(heroSectionId, productId);
            var result = await _mediator.Send(query);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<APIResponse<List<HeroSectionProductResponse>>> GetAllHeroSectionProduct()
        {
            var query = new GetAllHeroSectionProductQuery();
            var result = await _mediator.Send(query);
            if (result == null) return APIResponse<List<HeroSectionProductResponse>>.NotFound("Không có herosection product");
            return APIResponse<List<HeroSectionProductResponse>>.Success(result, "Lấy thành công"); 
        }

        //lọc hero section product
        [HttpGet("filter")]
        public async Task<APIResponse<List<HeroSectionProductResponse>>> Filter([FromQuery] HeroSectionProductFilterRequest filterRequest)
        {
            var query = new FilterHRPQuery(filterRequest);
            var result = await _mediator.Send(query);
            return APIResponse<List<HeroSectionProductResponse>>.Success(result, "Filtered hero section products successfully");
        }
    }
}
