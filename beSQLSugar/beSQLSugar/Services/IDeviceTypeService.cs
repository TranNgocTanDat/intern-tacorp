using beSQLSugar.DTO.request;
using beSQLSugar.DTO.response;
using beSQLSugar.models;

namespace beSQLSugar.Services
{
    public interface IDeviceTypeService
    {
        public Task<List<DeviceTypeResponse>> GetAllAsync();
        public Task<DeviceTypeResponse?> GetByIdAsync(int id);
        public Task<DeviceTypeResponse?> AddAsync(DeviceTypeRequest entity);
        public Task<int> UpdateAsync(DeviceType entity);
        public Task<int> DeleteAsync(int id);

    }
}
