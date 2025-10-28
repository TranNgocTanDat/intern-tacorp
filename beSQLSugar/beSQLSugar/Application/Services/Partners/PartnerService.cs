using AutoMapper;
using beSQLSugar.Application.Dto.request.Partner;
using beSQLSugar.Application.Dto.response.Partner;
using beSQLSugar.Application.Services.Helper;
using beSQLSugar.Infrastructure.Database.Enities;
using beSQLSugar.Infrastructure.Repositories.Partners;
using NetTaste;

namespace beSQLSugar.Application.Services.Partners
{
    public class PartnerService : IPartnerService
    {
        private readonly IPartnerRepository _partnerRepository;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContext;
        private readonly IWebHostEnvironment _env;
        public PartnerService(IPartnerRepository partnerRepository, IMapper mapper, IUserContextService userContext, IWebHostEnvironment env)
        {
            _partnerRepository = partnerRepository;
            _mapper = mapper;
            _userContext = userContext;
            _env = env;
        }

        public async Task<PartnerResponse> CreatePartnerAsync(PartnerRequest request)
        {
            int userId = _userContext.GetUserId();
            string userName = _userContext.GetUserName();
            var partner = _mapper.Map<Partner>(request);
            // Upload file nếu có
            if (request.LogoFile != null)
            {
                partner.LogoUrl = await SaveLogoFileAsync(request.LogoFile);
            }
            if( request.ImgDefaultFile != null)
            {
                partner.ImgDefaultUrl = await SaveLogoFileAsync(request.ImgDefaultFile);
            }
            if( request.ImgHoverFile != null)
            {
                partner.ImgHoverUrl = await SaveLogoFileAsync(request.ImgHoverFile);
            }

            partner.CreateUid = userId;
            partner.CreatedName = userName;

            var result = await _partnerRepository.AddAsync(partner);
            return _mapper.Map<PartnerResponse>(result);
        }

        public async Task<bool> DeletePartnerAsync(int id)
        {
            var existing = await _partnerRepository.GetByIdAsync(id);
            if (existing == null)
                throw new Exception("Partner not found");
            await _partnerRepository.DeleteAsync(id);
            return true;
        }

        public async Task<List<PartnerResponse>> FilterPartnersAsync(PartnerFilterRequest request)
        {
            var partners = await _partnerRepository.FilterPartnerAsync(request);
            return _mapper.Map<List<PartnerResponse>>(partners);
        }

        public async Task<List<PartnerResponse>> GetAllPartnersAsync()
        {
            var partners = await _partnerRepository.GetAllAsync();
            return _mapper.Map<List<PartnerResponse>>(partners);
        }

        public async Task<PartnerResponse> GetPartnerByIdAsync(int id)
        {
            var partner = await _partnerRepository.GetByIdAsync(id);
            return _mapper.Map<PartnerResponse>(partner);
        }

        public async Task<PartnerResponse> UpdatePartnerAsync(int id, PartnerRequest request)
        {
            int userId = _userContext.GetUserId();
            string userName = _userContext.GetUserName();
            var existing = await _partnerRepository.GetByIdAsync(id);
            if (existing == null)
                throw new Exception("Partner not found");

            _mapper.Map(request, existing);

            // Nếu có file mới thì lưu lại
            if (request.LogoFile != null)
            {
                existing.LogoUrl = await SaveLogoFileAsync(request.LogoFile);
            }
            if (request.ImgDefaultFile != null)
            {
                existing.ImgDefaultUrl = await SaveLogoFileAsync(request.ImgDefaultFile);
            }
            if (request.ImgHoverFile != null)
            {
                existing.ImgHoverUrl = await SaveLogoFileAsync(request.ImgHoverFile);
            }

            existing.WriteIUid =userId;
            existing.UpdatedName = userName;
            existing.UpdateTime = DateTime.Now;

            await _partnerRepository.UpdateAsync(existing);
            return _mapper.Map<PartnerResponse>(existing);
        }

    
        private async Task<string> SaveLogoFileAsync(IFormFile file)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "partners");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Trả về đường dẫn public để FE hiển thị
            return $"/uploads/partners/{fileName}";
        }
    }
}
