using AutoMapper;
using beSQLSugar.Application.Dto.request.ProductSpec;
using beSQLSugar.Application.Dto.response.ProductSpec;
using beSQLSugar.Application.Services.Helper;
using beSQLSugar.Infrastructure.Database.Enities;
using beSQLSugar.Infrastructure.Repositories.ProductSpecs;

namespace beSQLSugar.Application.Services.ProductSpecServivces
{
    public class ProductSpecService : IProductSpecService
    {
        private readonly IProductSpecRepository _productSpecRepository;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContext;
        public ProductSpecService(IProductSpecRepository productSpecRepository, IMapper mapper, IUserContextService userContext)
        {
            _productSpecRepository = productSpecRepository;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<ProductSpecResponse?> AddSpecAsync(int productId, ProductSpecRequest spec)
        {
            int userId = _userContext.GetUserId();
            string userName = _userContext.GetUserName();
            var entity = _mapper.Map<ProductSpec>(spec);
            entity.ProductId = productId;
            entity.CreateUid = userId;
            entity.CreatedName = userName;
            var created =await _productSpecRepository.AddAsync(entity);
            return _mapper.Map<ProductSpecResponse>(created);
        }

        public async Task<bool> DeleteSpecAsync(int id)
        {
            var spec = await _productSpecRepository.GetByIdAsync(id);
            if (spec == null) throw new Exception("Product spec not found");
            await _productSpecRepository.DeleteAsync(id);
            return true;
        }

        public async Task<List<ProductSpecResponse>> FilterProductSpec(ProductSpecFilterRequest request)
        {
            var specs = await _productSpecRepository.FilterProductSpecs(request);
            return _mapper.Map<List<ProductSpecResponse>>(specs);
        }

        public async Task<List<ProductSpecResponse>> GetAllSpecAsync()
        {
            var specs = await _productSpecRepository.GetAllWithProductAsync();
            return _mapper.Map<List<ProductSpecResponse>>(specs);
        }

        public async Task<List<ProductSpecResponse>> GetSpecsByProductIdAsync(int productId)
        {
            var specs = await _productSpecRepository.GetByProductIdAsync(productId);
            return _mapper.Map<List<ProductSpecResponse>>(specs);

        }

        public async Task<ProductSpecResponse?> UpdateSpecAsync(int productId, int specId, ProductSpecRequest spec)
        {
            int userId = _userContext.GetUserId();
            string userName = _userContext.GetUserName();

            var existingSpec = await _productSpecRepository.GetByIdAsync(specId);
            if (existingSpec == null || existingSpec.ProductId != productId) return null;

            _mapper.Map(spec, existingSpec);
            existingSpec.UpdatedName = userName;
            existingSpec.UpdateTime = DateTime.UtcNow;

            await _productSpecRepository.UpdateAsync(existingSpec);

            return _mapper.Map<ProductSpecResponse>(existingSpec);
        }

    }
}
