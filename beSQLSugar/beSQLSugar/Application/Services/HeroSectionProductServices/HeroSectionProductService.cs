using AutoMapper;
using beSQLSugar.Application.Dto.request.HeroSectionProduct;
using beSQLSugar.Application.Dto.response.HeroSectionProduct;
using beSQLSugar.Infrastructure.Database.Enities;
using beSQLSugar.Infrastructure.Repositories.HeroSectionProducts;
using System.Security.Claims;

namespace beSQLSugar.Application.Services.HeroSectionProductServices

{
    public class HeroSectionProductService : IHeroSectionProductService
    {
        private readonly IHeroSectionProductRepository _repository;
        private readonly IMapper _mapper;
        public HeroSectionProductService(IHeroSectionProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        // Thêm mới (chỉ truyền request)
        public async Task<HeroSectionProductResponse?> AddAsync(HeroSectionProductRequest request, ClaimsPrincipal admin)
        {
            var userIdClaim = admin.FindFirst("uid");
            if (userIdClaim == null) throw new UnauthorizedAccessException("Không tìm thấy thông tin người dùng trong token");

            int adminUserId = int.Parse(userIdClaim.Value);

            var userNameClaim = admin.FindFirst(ClaimTypes.Name);
            string adminName = userNameClaim?.Value ?? "Unknown";
            // Check trùng (HeroSectionId + ProductId)
            var existing = await _repository.GetByHeroSectionAndProductAsync(request.HeroSectionId, request.ProductId);
            if (existing != null)
            {
                throw new Exception("HeroSectionProduct đã tồn tại");
            }

            var entity = _mapper.Map<HeroSectionProduct>(request);
            entity.CreateUid = adminUserId;
            entity.CreatedName = adminName;
            var result = await _repository.AddAsync(entity);

            return _mapper.Map<HeroSectionProductResponse?>(result);
        }

        // Update theo Id
        public async Task<HeroSectionProductResponse> UpdateAsync(int id, HeroSectionProductRequest request, ClaimsPrincipal admin)
        {
            var userIdClaim = admin.FindFirst("uid");
            if (userIdClaim == null) throw new UnauthorizedAccessException("Không tìm thấy thông tin người dùng trong token");

            int adminUserId = int.Parse(userIdClaim.Value);

            var userNameClaim = admin.FindFirst(ClaimTypes.Name);
            string adminName = userNameClaim?.Value ?? "Unknown";
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new Exception("HeroSectionProduct không tồn tại");

            entity.HeroSectionId = request.HeroSectionId;
            entity.ProductId = request.ProductId;
            entity.UpdatedName = adminName;
            entity.UpdateTime = DateTime.Now;

            await _repository.UpdateAsync(entity);

            return _mapper.Map<HeroSectionProductResponse>(entity);
        }

        // Xóa theo Id
        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            return deleted > 0;
        }

        // Lấy danh sách HeroSectionProduct theo HeroSectionId
        public async Task<List<HeroSectionProductResponse>> GetByHeroSectionIdAsync(int heroSectionId)
        {
            var entities = await _repository.GetByHeroSectionIdAsync(heroSectionId);
            return _mapper.Map<List<HeroSectionProductResponse>>(entities);
        }

        // Lấy HeroSectionProduct theo HeroSectionId + ProductId
        public async Task<HeroSectionProductResponse?> GetByHeroSectionAndProductAsync(int heroSectionId, int productId)
        {
            var entity = await _repository.GetByHeroSectionAndProductAsync(heroSectionId, productId);
            return _mapper.Map<HeroSectionProductResponse?>(entity);
        }

        public async Task<List<HeroSectionProductResponse>> GetAllHRPAsync()
        {
            var entity = await _repository.GetAllHRPAsync();
            return _mapper.Map<List<HeroSectionProductResponse>>(entity);
        }

        public async Task<HeroSectionProductResponse?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<HeroSectionProductResponse?>(entity);
        }

        public Task<List<HeroSectionProductResponse>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        // Lọc HeroSectionProduct
        public async Task<List<HeroSectionProductResponse>> FilterAsync(HeroSectionProductFilterRequest filterRequest)
        {
            var entities = await _repository.FilterAsync(filterRequest);
            return _mapper.Map<List<HeroSectionProductResponse>>(entities);
        }
    }
}
