using beSQLSugar.Common;
using beSQLSugar.DTO.request;
using beSQLSugar.DTO.response;
using beSQLSugar.Services;
using Microsoft.AspNetCore.Mvc;

namespace beSQLSugar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceTypeControler : ControllerBase
    {
        private readonly IDeviceTypeService _deviceTypeService;
        public DeviceTypeControler(IDeviceTypeService deviceTypeService)
        {
            _deviceTypeService = deviceTypeService;
        }

        [HttpPost]
        public async Task<IActionResult> createDeviceType([FromBody] DeviceTypeRequest request)
        {
            try
            {
                var deviceType = await _deviceTypeService.AddAsync(request);
                return Ok(APIResponse<DeviceTypeResponse>.Success(deviceType, "Device type created successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(APIResponse<DeviceTypeResponse>.Fail(ex.Message));
            }
        }
    }
}
