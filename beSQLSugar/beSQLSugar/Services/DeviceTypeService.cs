using AutoMapper;
using beSQLSugar.DTO.request;
using beSQLSugar.DTO.response;
using beSQLSugar.models;
using beSQLSugar.Repository;

namespace beSQLSugar.Services
{
    public class DeviceTypeService : IDeviceTypeService
    {
        private readonly IRepository<DeviceType> _deviceTypeRepository;
        private readonly IMapper _mapper;

        public DeviceTypeService(IRepository<DeviceType> deviceTypeRepository, IMapper mapper)
        {
            _deviceTypeRepository = deviceTypeRepository;
            _mapper = mapper;
        }

        public async Task<DeviceTypeResponse?> AddAsync(DeviceTypeRequest entity)
        {
            var deviceType = _mapper.Map<DeviceType>(entity);
            var addedDeviceType = await _deviceTypeRepository.AddAsync(deviceType);
            return _mapper.Map<DeviceTypeResponse>(addedDeviceType);
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<DeviceTypeResponse>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DeviceTypeResponse?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(DeviceType entity)
        {
            throw new NotImplementedException();
        }
    }
}
