using AutoMapper;
using Azure.Core;
using beSQLSugar.Application.Dto.request.ProductMedia;
using beSQLSugar.Application.Dto.response.ProductMedia;
using beSQLSugar.Application.Services.Helper;
using beSQLSugar.Infrastructure.Database.Enities;
using beSQLSugar.Infrastructure.Repositories.ProductMedias;
using Microsoft.Identity.Client;

namespace beSQLSugar.Application.Services.ProductMediaServices
{
    public class ProductMediaService : IProductMediaService
    {
        private readonly IProductMediaRepository _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IUserContextService _userContext;
        public ProductMediaService(IProductMediaRepository repository, IMapper mapper, IWebHostEnvironment env, IUserContextService userContext)
        {
            _repository = repository;
            _mapper = mapper;
            _env = env;
            _userContext = userContext;
        }

        public async Task<List<ProductMediaResponse>> GetAllMediaAsync()
        {
            var mediaList = await _repository.GetAllWithProductAsync();
            return _mapper.Map<List<ProductMediaResponse>>(mediaList);
        }
        public async Task<List<ProductMediaResponse>> GetMediaByProductIdAsync(int productId)
        {
            var mediaList = await _repository.GetByProductIdAsync(productId);
            return _mapper.Map<List<ProductMediaResponse>>(mediaList);
        }


        public async Task<ProductMediaResponse?> AddMediaAsync(int productId, ProductMediaRequest request)
        {
            int userId = _userContext.GetUserId();
            string userName = _userContext.GetUserName();
            var entity = _mapper.Map<ProductMedia>(request);
            entity.ProductId = productId;
            entity.CreateUid = userId;
            entity.CreatedName = userName;

            if (request.MediaFileUrl != null && request.MediaFileUrl.Length > 0)
            {
                // Tạo thư mục uploads trong wwwroot
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Tạo tên file duy nhất
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(request.MediaFileUrl.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.MediaFileUrl.CopyToAsync(fileStream);
                }

                // Lưu URL (client sẽ gọi qua http://localhost:5271/uploads/...)
                entity.MediaFileUrl = $"/uploads/{uniqueFileName}";
                
            }

            var inserted = await _repository.AddAsync(entity);
            return _mapper.Map<ProductMediaResponse>(inserted); 
        }

        public async Task<ProductMediaResponse?> UpdateMediaAsync(int productId, int mediaId, ProductMediaRequest request)
        {

            string userName = _userContext.GetUserName();
            var media = await _repository.GetByIdAsync(mediaId);
            if (media == null)
                throw new Exception("Product media not found");

            if (media.ProductId != productId)
                throw new Exception("This media does not belong to the specified product");

            // Map các field từ request sang media (trừ MediaFileUrl nếu null)
            _mapper.Map(request, media);

            // Nếu có upload file mới thì thay thế
            if (request.MediaFileUrl != null && request.MediaFileUrl.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(request.MediaFileUrl.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.MediaFileUrl.CopyToAsync(fileStream);
                }

                media.MediaFileUrl = $"/uploads/{uniqueFileName}";
            }
            media.UpdatedName = userName;
            media.UpdateTime = DateTime.UtcNow;

            await _repository.UpdateAsync(media);

            var updatedMedia = await _repository.GetByIdAsync(media.Id);
            return _mapper.Map<ProductMediaResponse>(updatedMedia);
        }


        public async Task<bool> DeleteMediaAsync(int id)
        {
            var media = await _repository.GetByIdAsync(id);
            if (media == null) throw new Exception("Product media not found");
            await _repository.DeleteAsync(id);
            return true;
        }

        public async Task<List<ProductMediaResponse>> FilterProductMedia(ProductMediaFilterRequest request)
        {
            var mediaList = await _repository.FilterProductMedia(request);
            return _mapper.Map<List<ProductMediaResponse>>(mediaList);
        }
    }
}
