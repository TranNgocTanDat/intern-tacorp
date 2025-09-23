using AutoMapper;
using beSQLSugar.Application.DTO.request;
using beSQLSugar.Application.DTO.response;
using beSQLSugar.Application.DTOs.request;
using beSQLSugar.Application.ServiceInterfaces;
using beSQLSugar.Domain.Enities;
using beSQLSugar.Domain.RepositoryInterfaces;

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

        public async Task<CategoryResponse?> AddAsync(CategoryRequest request)
        {
            // Validate
            var existing = await _repository.GetByNameAsync(request.Name);
            if (existing != null) throw new Exception("Category name already exists");
            // Map DTO to entity
            var category = _mapper.Map<Category>(request);
            // Save entity and return DTO response
            await _repository.AddAsync(category);
            return _mapper.Map<CategoryResponse>(category);

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
            var categories = await _repository.GetAllAsync();
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

        public async Task<CategoryResponse> UpdateAsync(int id, CategoryRequest request)
        {
            // Check if entity exists
            var category = await _repository.GetByIdAsync(id);
            if (category == null) throw new Exception("Category not found");
            // Map updated fields from DTO to entity
            var updatedCategory = _mapper.Map<Category>(request);

            // Câp nhật entity
            await _repository.UpdateAsync(updatedCategory);
            return _mapper.Map<CategoryResponse>(updatedCategory);
        }
    }
}
