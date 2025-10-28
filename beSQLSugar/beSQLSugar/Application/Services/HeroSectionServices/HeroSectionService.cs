using AutoMapper;
using Azure;
using beSQLSugar.Application.Dto.request.HeroSection;
using beSQLSugar.Application.Dto.response.HeroSection;
using beSQLSugar.Application.Features.AdminUsers.Queries;
using beSQLSugar.Infrastructure.Database.Enities;
using beSQLSugar.Infrastructure.Repositories;
using beSQLSugar.Infrastructure.Repositories.AdminRepository;
using beSQLSugar.Infrastructure.Repositories.HeroSections;
using System.Security.Claims;

namespace beSQLSugar.Application.Services.HeroSectionServices
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

        public async Task<HeroSectionResponse?> AddAsync(HeroSectionRequest request, ClaimsPrincipal admin)
        {
            var userIdClaim = admin.FindFirst("uid");
            if (userIdClaim == null) throw new UnauthorizedAccessException("Không tìm thấy thông tin người dùng trong token");

            int adminUserId = int.Parse(userIdClaim.Value);

            var userNameClaim = admin.FindFirst(ClaimTypes.Name);
            string adminName = userNameClaim?.Value ?? "Unknown";
            // Nếu không truyền PublishFrom, thì mặc định lấy thời điểm hiện tại
            if (request.PublishFrom == null)
            {
                request.PublishFrom = DateTime.Now;
            }

            // Nếu không truyền PublishTo, hoặc PublishTo <= PublishFrom, thì mặc định là 7 ngày sau PublishFrom
            if (request.PublishTo == null || request.PublishTo <= request.PublishFrom)
            {
                request.PublishTo = request.PublishFrom.Value.AddDays(7);
            }

            if (request.IsPublished && request.PublishFrom > request.PublishTo)
            {
                throw new Exception("Thời điểm bắt đầu phải nhỏ hơn ngày kết thúc");
            }

            var entity = _mapper.Map<HeroSection>(request);
            entity.CreateUid = adminUserId;
            entity.CreatedName = adminName;

            // Xử lý upload media như cũ...
            if (request.HeroMediaFile != null && request.HeroMediaFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(request.HeroMediaFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.HeroMediaFile.CopyToAsync(fileStream);
                }

                entity.HeroMediaUrl = $"/uploads/{uniqueFileName}";
                entity.HeroMediaType = request.HeroMediaFile.ContentType.StartsWith("video") ? "video" : "image";
            }

            var addedEntity = await _repository.AddAsync(entity);
            return _mapper.Map<HeroSectionResponse>(addedEntity);
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

        // Filter HeroSection theo các tiêu chí trong request
        public async Task<List<HeroSectionResponse>> FilterAsync(HeroSectionFilterRequest request)
        {
            // Lấy tất cả entity
            var entities = await _repository.FilterAsync(request);

            // Map sang danh sách DTO và trả về
            return _mapper.Map<List<HeroSectionResponse>>(entities);
        }

        // Cập nhật entity
        public async Task<HeroSectionResponse> UpdateAsync(int id, HeroSectionRequest request, ClaimsPrincipal admin)
        {
            var userIdClaim = admin.FindFirst("uid");
            if (userIdClaim == null)
                throw new UnauthorizedAccessException("Không tìm thấy thông tin người dùng trong token");

            int adminUserId = int.Parse(userIdClaim.Value);
            var userNameClaim = admin.FindFirst(ClaimTypes.Name);
            string adminName = userNameClaim?.Value ?? "Unknown";

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException("HeroSection not found");

            // Map tất cả các trường từ request vào existing (trừ media file)
            _mapper.Map(request, existing);

            // Nếu có file mới thì xử lý upload và cập nhật đường dẫn + loại media
            if (request.HeroMediaFile != null && request.HeroMediaFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(request.HeroMediaFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.HeroMediaFile.CopyToAsync(fileStream);
                }

                existing.HeroMediaUrl = $"/uploads/{uniqueFileName}";
                existing.HeroMediaType = request.HeroMediaFile.ContentType.StartsWith("video") ? "video" : "image";
            }

            // Cập nhật thông tin người sửa
            existing.WriteIUid = adminUserId;
            existing.UpdatedName = adminName;
            existing.UpdateTime = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(existing);
            if (updated == 0)
                throw new Exception("Update failed");

            var result = await _repository.GetByIdAsync(existing.Id);
            return _mapper.Map<HeroSectionResponse>(result);
        }

        public async Task<HeroSectionResponse?> GetHeroSectionWithDetailsAsync(int id)
        {
            var entities = await _repository.GetHeroSectionWithDetailsAsync(id);
            return _mapper.Map<HeroSectionResponse?>(entities);
        }

        public async Task<List<HeroSectionResponse>> GetAllWithDetailsAsync()
        {
            var entities = await _repository.GetAllWithDetailsAsync();
            return _mapper.Map<List<HeroSectionResponse>>(entities);
        }

        public async Task<List<HeroSectionResponse>> GetHeroSectionsWithPageHeroAsync(string pageHero)
        {
            var entities = await _repository.GetHeroSectionsWithPageHeroAsync(pageHero);
            return _mapper.Map<List<HeroSectionResponse>>(entities);
        }
    }
}
