using AutoMapper;
using Azure;
using beSQLSugar.Application.DTO.request;
using beSQLSugar.Application.DTO.response;
using beSQLSugar.Application.DTOs.request;
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

            if (request.HeroMediaFile != null && request.HeroMediaFile.Length > 0)
            {
                // Tạo thư mục uploads trong wwwroot
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Tạo tên file duy nhất
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(request.HeroMediaFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.HeroMediaFile.CopyToAsync(fileStream);
                }

                // Lưu URL (client sẽ gọi qua http://localhost:5271/uploads/...)
                entity.HeroMediaUrl = $"/uploads/{uniqueFileName}";
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

        // Lấy HeroSection theo id
        public async Task<HeroSectionResponse?> GetByIdAsync(int id)
        {
            
            var entity = await _repository.GetByIdAsync(id);
            return  _mapper.Map<HeroSectionResponse?>(entity);
        }

        // Filter HeroSection theo các tiêu chí trong request
        public async Task<List<HeroSectionResponse>> FilterAsync(HeroSectionFilterRequest request)
        {
            // Lấy tất cả entity
            var entities = await _repository.FilterAsync(request);
            
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

            if (request.HeroMediaFile != null && request.HeroMediaFile.Length > 0)
            {
                // Tạo thư mục uploads trong wwwroot
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Tạo tên file duy nhất
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(request.HeroMediaFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.HeroMediaFile.CopyToAsync(fileStream);
                }

                // Lưu URL (client sẽ gọi qua http://localhost:5271/uploads/...)
                heroSection.HeroMediaUrl = $"/uploads/{uniqueFileName}";
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
