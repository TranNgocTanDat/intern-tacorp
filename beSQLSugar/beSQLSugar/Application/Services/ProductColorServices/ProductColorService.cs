using AutoMapper;
using beSQLSugar.Application.Dto.request.ProductColor;
using beSQLSugar.Application.Dto.response.ProductColor;
using beSQLSugar.Application.Services.Helper;
using beSQLSugar.Infrastructure;
using beSQLSugar.Infrastructure.Database.Enities;
using beSQLSugar.Infrastructure.Repositories.ProductColors;

namespace beSQLSugar.Application.Services.ProductColorServices
{
    public class ProductColorService : IProductColorService
    {
        private readonly IProductColorRepository _productColorRepository;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContext;
        public ProductColorService(IProductColorRepository productColorRepository, IMapper mapper, IUserContextService userContext)
        {
            _productColorRepository = productColorRepository;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<ProductColorResponse> CreateAsync(ProductColorRequest request)
        {
            var productColor = _mapper.Map<ProductColor>(request);
            productColor.ProductId = request.ProductId;
            productColor.CreateUid = _userContext.GetUserId();
            productColor.CreatedName = _userContext.GetUserName();

            var newProductColor = await _productColorRepository.AddAsync(productColor);
            return _mapper.Map<ProductColorResponse>(newProductColor);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingProductColor = await _productColorRepository.GetByIdAsync(id);
            if (existingProductColor == null)
            {
                return false; // Hoặc ném ngoại lệ nếu muốn
            }
            await _productColorRepository.DeleteAsync(id);
            return true;
        }

        public async Task<List<ProductColorResponse>> FilterProductColorAsync(ProductColorFilterRequest request)
        {
            var productColors = await _productColorRepository.FilterProductColorAsync(request);
            return _mapper.Map<List<ProductColorResponse>>(productColors);
        }

        public async Task<List<ProductColorResponse>> GetAllAsync()
        {
            var productColors = await _productColorRepository.GetAllWithProductAsync();
            return _mapper.Map<List<ProductColorResponse>>(productColors);
        }

        public async Task<List<ProductColorResponse>> GetAvailableColorsByProductIdAsync(int productId)
        {
            var colors = await _productColorRepository.GetAvailableColorsByProductIdAsync(productId);
            return _mapper.Map<List<ProductColorResponse>>(colors);
        }

        public async Task<List<ProductColorResponse>> GetByProductIdAsync(int productId)
        {
            var productColors = await _productColorRepository.GetByProductIdAsync(productId);
            return _mapper.Map<List<ProductColorResponse>>(productColors);
        }

        public async Task<List<ProductColorResponse>> GetByProductIdAndColorIdAsync(int productId, int colorId)
        {
            var productColors = await _productColorRepository.GetByProductIdAndColorIdAsync(productId, colorId);
            return _mapper.Map<List<ProductColorResponse>>(productColors);
        }

        public async Task<ProductColorResponse> UpdateAsync(int id, ProductColorRequest request)
        {
            var existingProductColor = await _productColorRepository.GetByIdAsync(id);
            if (existingProductColor == null)
            {
                throw new Exception("Product color not found"); // Hoặc ném ngoại lệ tùy chỉnh
            }
            _mapper.Map(request, existingProductColor);
            existingProductColor.ProductId = request.ProductId;
            existingProductColor.WriteIUid = _userContext.GetUserId();
            existingProductColor.UpdatedName = _userContext.GetUserName();
            existingProductColor.UpdateTime = DateTime.Now;
            await _productColorRepository.UpdateAsync(existingProductColor);
            return _mapper.Map<ProductColorResponse>(existingProductColor);

        }
    }
}
