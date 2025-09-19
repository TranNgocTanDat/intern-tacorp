using AutoMapper;
using Azure;
using beSQLSugar.Application.DTO.request;
using beSQLSugar.Application.DTO.response;
using beSQLSugar.Application.ServiceInterfaces;
using beSQLSugar.Domain.Enities;
using beSQLSugar.Domain.RepositoryInterfaces;
using beSQLSugar.Infrastructure.Repositories;

namespace beSQLSugar.Application.Services
{
    // Triển khai các phương thức trong IHeroSectionService
    public class HeroSectionService : IHeroSectionService
    {
        private readonly IHeroSectionRepository _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public HeroSectionService(IHeroSectionRepository repository, IMapper mapper, IWebHostEnvironment env)
        {
            _repository = repository;
            _mapper = mapper;
            _env = env;
        }

        public async Task<HeroSectionResponse?> AddAsync(HeroSectionRequest request)
        {
            // Validate
            if (request.IsPublished && request.PublishFrom != null && request.PublishTo != null && request.PublishFrom > request.PublishTo)
            {
                throw new Exception("Thời điểm bắt đầu phải nhỏ hơn ngày kết thúc");
            }

            // Map DTO sang entity
            var entity = _mapper.Map<HeroSection>(request);

            if(request.HeroMediaFile != null && request.HeroMediaFile.Length > 0)
            {
                // Tạo đường dẫn lưu ảnh
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Tạo tên file duy nhất
                var originalFileName = Path.GetFileName(request.HeroMediaFile.FileName);
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(originalFileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                // Lưu file vào đường dẫn
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.HeroMediaFile.CopyToAsync(fileStream);
                }
                // Lưu đường dẫn ảnh vào entity
                entity.HeroMediaUrl = "/uploads/" + uniqueFileName;
                entity.HeroMediaType = request.HeroMediaFile.ContentType.StartsWith("video") ? "video" : "image";
            }

            // Thêm entity vào repository
            var addedEntity = await _repository.AddAsync(entity);

            // Map entity vừa thêm sang DTO response
            var response = _mapper.Map<HeroSectionResponse>(addedEntity);

            return response;
        }

        // Xóa entity theo id
        public async Task<bool> DeleteAsync(int id)
        {
            // Lây entity theo id
            var heroSection = await _repository.GetByIdAsync(id);
            if (heroSection == null) throw new Exception("HeroSection not found");
            // Xóa entity
            await _repository.DeleteAsync(id);
            return true;

        }

        // Lấy tất cả danh sách HeroSection
        public async Task<List<HeroSectionResponse>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<HeroSectionResponse>>(entities);
        }

        // Lấy danh sách HeroSection theo khoảng thời gian
        public async Task<List<HeroSectionResponse>> GetByDateRangeAsync(DateTime from, DateTime to)
        {
            // Lấy danh sách entity từ repository
            var entities = await _repository.GetByDateRangeAsync(from, to);
            // Map sang danh sách DTO và trả về
            return _mapper.Map<List<HeroSectionResponse>>(entities);
        }

        // Lấy HeroSection theo id
        public async Task<HeroSectionResponse?> GetByIdAsync(int id)
        {
            
            var entity = await _repository.GetByIdAsync(id);
            return  _mapper.Map<HeroSectionResponse?>(entity);
        }

        // Lấy danh sách HeroSection theo pageHero
        public async Task<List<HeroSectionResponse>> GetByPageHeroAsync(string pageHero)
        {
            // Lấy danh sách entity từ repository
            var entities = await _repository.GetByPageHeroAsync(pageHero);
            // Map sang danh sách DTO và trả về
            return  _mapper.Map<List<HeroSectionResponse>>(entities);
        }

        // Lấy danh sách HeroSection đang được public
        public async Task<List<HeroSectionResponse>> GetPublishedAsync()
        {
            // Lấy danh sách entity từ repository
            var entities = await _repository.GetPublishedAsync();
            // Map sang danh sách DTO và trả về
            return  _mapper.Map<List<HeroSectionResponse>>(entities);
        }

        // Lấy HeroSection cùng với danh sách HeroProduct liên quan
        public async Task<HeroSectionResponse?> GetWithProductsAsync(int id)
        {
            // Lấy entity từ repository
            var entity = await _repository.GetWithProductsAsync(id);
            // Map sang DTO hoặc trả về null nếu không tìm thấy
            return  _mapper.Map<HeroSectionResponse?>(entity);
        }

        public async Task<List<HeroSectionResponse>> SearchAsync(string keyword)
        {
            // Lấy danh sách entity từ repository
            var entities = await _repository.SearchAsync(keyword);
            // Map sang danh sách DTO và trả về
            return  _mapper.Map<List<HeroSectionResponse>>(entities);
        }

        // Cập nhật entity
        public async Task<HeroSectionResponse> UpdateAsync(int id, HeroSectionRequest request)
        {
            // Validate
            if (request.IsPublished && request.PublishFrom != null && request.PublishTo != null && request.PublishFrom > request.PublishTo)
            {
                throw new Exception("Thời điểm bắt đầu phải nhỏ hơn ngày kết thúc");
            }

            // 2. Check tồn tại
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
            {
                throw new KeyNotFoundException("HeroSection not found");
            }

            // Map DTO sang entity
            var heroSection = _mapper.Map<HeroSection>(request);
            heroSection.Id = id;

            if (request.HeroMediaFile != null)
            {
                var fileName = $"{Guid.NewGuid()}_{request.HeroMediaFile.FileName}";
                var folder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                var filePath = Path.Combine(folder, fileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                await request.HeroMediaFile.CopyToAsync(stream);

                heroSection.HeroMediaUrl = $"/uploads/{fileName}";
                heroSection.HeroMediaType = request.HeroMediaFile.ContentType.StartsWith("video") ? "video" : "image";
            }

            // Cập nhật entity
            var updated = await _repository.UpdateAsync(heroSection);

            if (updated == 0)
                throw new Exception("Update failed");

            var result = await _repository.GetByIdAsync(heroSection.Id);

            // Map entity vừa cập nhật sang DTO response
            var response = _mapper.Map<HeroSectionResponse>(result);

            return response;
        }
    }
}
