using AutoMapper;
using beSQLSugar.Application.Dto.request.Category;
using beSQLSugar.Application.Dto.response.Category;
using beSQLSugar.Application.Services.CategoryServices;
using beSQLSugar.Infrastructure.Database.Enities;
using beSQLSugar.Infrastructure.Repository.CategoryRepository;
using System.Security.Claims;

namespace beSQLSugar.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CategoryResponse?> AddAsync(CategoryRequest request, ClaimsPrincipal admin)
        {
            var userIdClaim = admin.FindFirst("uid");
            if (userIdClaim == null) throw new UnauthorizedAccessException("Không tìm thấy thông tin người dùng trong token");

            int adminUserId = int.Parse(userIdClaim.Value);

            var userNameClaim = admin.FindFirst(ClaimTypes.Name);
            string adminName = userNameClaim?.Value ?? "Unknown";
            // Validate
            var existing = await _repository.GetByNameAsync(request.Name);
            if (existing != null) throw new Exception("Category name already exists");

            // Xử lý ParentId
            if (request.ParentId == 0)
            {
                request.ParentId = null;
            }

            // Map DTO to entity
            var category = _mapper.Map<Category>(request);
            category.CreateUid = adminUserId;
            category.CreatedName = adminName;

            // Save entity and return DTO response
            var created = await _repository.AddAsync(category);
            return _mapper.Map<CategoryResponse>(created);

        }
        public async Task<bool> DeleteAsync(int id)
        {
            // Check if entity exists
            var category = await _repository.GetByIdAsync(id);
            if (category == null) throw new Exception("Category not found");
            // Delete entity
            await _repository.DeleteAsync(id);
            return true;
        }

        public async Task<List<CategoryResponse>> FilterAsync(CategoryFilterRequest request)
        {
            // Get filtered entities from repository
            var categories = await _repository.FilterAsync(request);
            // Map to list of DTO and return
            return _mapper.Map<List<CategoryResponse>>(categories);

        }

        public async Task<List<CategoryResponse>> GetAllAsync()
        {
            // Get all entities from repository
            var categories = await _repository.GetCategoriesAsync();
            // Map to list of DTO and return
            return _mapper.Map<List<CategoryResponse>>(categories);

        }

        public async Task<CategoryResponse?> GetByIdAsync(int id)
        {
            // Get entity by id from repository
            var category = await _repository.GetByIdAsync(id);
            // Map to DTO or return null if not found
            return category == null ? null : _mapper.Map<CategoryResponse>(category);

        }

        public async Task<CategoryResponse?> UpdateAsync(int id, CategoryRequest request, ClaimsPrincipal admin)
        {
            var userIdClaim = admin.FindFirst("uid");
            if (userIdClaim == null) throw new UnauthorizedAccessException("Không tìm thấy thông tin người dùng trong token");

            int adminUserId = int.Parse(userIdClaim.Value);

            var userNameClaim = admin.FindFirst(ClaimTypes.Name);
            string adminName = userNameClaim?.Value ?? "Unknown";
            // Check if entity exists
            var category = await _repository.GetByIdAsync(id);
            if (category == null) throw new Exception("Category not found");

            // Xử lý ParentId
            if (request.ParentId == 0)
            {
                request.ParentId = null;
            }
            // Map updated fields from DTO to entity
            var updatedCategory = _mapper.Map<Category>(request);
            updatedCategory.Id = id;
            updatedCategory.UpdatedName = adminName;
            updatedCategory.UpdateTime = DateTime.Now;
            // Câp nhật entity
            await _repository.UpdateAsync(updatedCategory);
            return _mapper.Map<CategoryResponse>(updatedCategory);
        }

        public async Task<List<CategoryResponse>> GetAllWithDetailsAsync()
        {
            var categories = await _repository.GetAllWithDetailsAsync();
            return _mapper.Map<List<CategoryResponse>>(categories);
        }

        public async Task<CategoryResponse?> GetCategoryWithDetailsAsync(int id)
        {
            var category = await _repository.GetCategoryWithDetailsAsync(id);
            return category == null ? null : _mapper.Map<CategoryResponse>(category);
        }

        public async Task<List<CategoryResponse>> GetCategoryChildrenAsync()
        {
            var categories = await _repository.GetCategoryChildrenAsync();
            return _mapper.Map<List<CategoryResponse>>(categories);
        }
    }
}
