using beSQLSugar.Application.Dto.request.Contact;
using beSQLSugar.Application.Dto.response.Contact;
using beSQLSugar.Application.Features.Contact.Commands;
using beSQLSugar.Application.Features.Contact.Queries;
using beSQLSugar.Share.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace beSQLSugar.API
{
    [ApiController]
    [Route("api/contact")]
    public class ContactController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Tạo liên hệ mới
        [HttpPost]
        public async Task<APIResponse<ContactResponse>> CreateContact([FromBody] ContactRequest request)
        {
            var command = new CreateContactCommand(request);
            var result = await _mediator.Send(command);
            return APIResponse<ContactResponse>.Created(result, "Tạo liên hệ thành công");
        }

        // Xóa liên hệ
        [HttpDelete("{id}")]
        public async Task<APIResponse<bool>> DeleteContact(int id)
        {
            var command = new DeleteContactCommand(id);
            var result = await _mediator.Send(command);
            return APIResponse<bool>.Success(result, "Xóa liên hệ thành công");
        }

        // Cập nhật trạng thái đã liên hệ
        [HttpPut("{id}/status")]
        public async Task<APIResponse<ContactResponse>> UpdateContactStatus(int id, [FromBody] UpdateContactStatusRequest request)
        {
            var command = new UpdateContactStatusCommand(id, request);
            var result = await _mediator.Send(command);
            return APIResponse<ContactResponse>.Success(result, "Cập nhật trạng thái liên hệ thành công");
        }

        // Lấy tất cả liên hệ có product
        [HttpGet]
        public async Task<APIResponse<List<ContactResponse>>> GetAllWithProduct()
        {
            var query = new GetAllWithProductQuery();
            var result = await _mediator.Send(query);
            return APIResponse<List<ContactResponse>>.Success(result, "Lấy tất cả liên hệ thành công");
        }

        // Lấy liên hệ theo id với product
        [HttpGet("{id}")]
        public async Task<APIResponse<ContactResponse>> GetByIdWithProduct(int id)
        {
            var query = new GetByIdWithProductQuery(id);
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return APIResponse<ContactResponse>.NotFound("Không tìm thấy liên hệ với ID này.");
            }
            return APIResponse<ContactResponse>.Success(result, "Lấy liên hệ thành công");
        }

        // Lấy liên hệ theo trạng thái
        [HttpGet("status/{status}")]
        public async Task<APIResponse<List<ContactResponse>>> GetByStatus(string status)
        {
            var query = new GetByStatusQuery(status);
            var result = await _mediator.Send(query);
            return APIResponse<List<ContactResponse>>.Success(result, "Lấy liên hệ theo trạng thái thành công");
        }

        // lọc liên hệ
        [HttpGet("filter")]
        public async Task<APIResponse<List<ContactResponse>>> FilterContacts([FromQuery] ContactFilterRequest filterRequest)
        {
            var query = new FilterContactQuery(filterRequest);
            var result = await _mediator.Send(query);
            return APIResponse<List<ContactResponse>>.Success(result, "Lọc liên hệ thành công");
        }
    }
}
