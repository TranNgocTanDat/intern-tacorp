using AutoMapper;
using beSQLSugar.Application.Dto.request.ProductStorage;
using beSQLSugar.Application.Dto.response.ProductStorage;
using beSQLSugar.Application.Services.Helper;
using beSQLSugar.Infrastructure.Database.Enities;
using beSQLSugar.Infrastructure.Repositories.ProductColors;
using beSQLSugar.Infrastructure.Repositories.ProductStorages;

namespace beSQLSugar.Application.Services.ProductStorageServices
{
    public class ProductStorageService : IProductStorageService
    {
        private readonly IProductStorageRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContext;
        public ProductStorageService(IProductStorageRepository repository, IMapper mapper, IUserContextService userContext)
        {
            _repository = repository;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<ProductStorageResponse> CreateAsync(ProductStorageRequest request)
        {
            var entity = _mapper.Map<ProductStorage>(request);
            entity.ProductId = request.ProductId;
            entity.CreateUid = _userContext.GetUserId();
            entity.CreateUid = _userContext.GetUserId();
            var createdEntity = await _repository.AddAsync(entity);
            return _mapper.Map<ProductStorageResponse>(createdEntity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingEntity = await _repository.GetByIdAsync(id);
            if (existingEntity == null)
            {
                return false; // Hoặc ném ra ngoại lệ nếu bạn muốn
            }
            await _repository.DeleteAsync(id);
            return true;

        }

        public async Task<List<ProductStorageResponse>> FilterProductStorageAsync(ProductStorageFilterRequest request)
        {
            var entities = await _repository.FilterProductStorageAsync(request);
            return _mapper.Map<List<ProductStorageResponse>>(entities);
        }

        public async Task<List<ProductStorageResponse>> GetAllWithProductAsync()
        {
            var entities = await _repository.GetAllWithProductAsync();
            return _mapper.Map<List<ProductStorageResponse>>(entities);
        }

        public async Task<List<ProductStorageResponse>> GetByIdAsync(int id)
        {
            var entities = await _repository.GetByProductIdAsync(id);
            return _mapper.Map<List<ProductStorageResponse>>(entities);

        }

        public async Task<ProductStorageResponse> UpdateAsync(int id, ProductStorageRequest request)
        {
            var existingEntity = await _repository.GetByIdAsync(id);
            if (existingEntity == null)
            {
                throw new Exception("Product storage not found");
            }
            _mapper.Map(request, existingEntity);
            existingEntity.ProductId = request.ProductId;
            existingEntity.WriteIUid = _userContext.GetUserId();
            existingEntity.UpdatedName = _userContext.GetUserName();
            existingEntity.UpdateTime = DateTime.Now;
            var updatedEntity = await _repository.UpdateAsync(existingEntity);
            return _mapper.Map<ProductStorageResponse>(updatedEntity);
        }
    }
}
