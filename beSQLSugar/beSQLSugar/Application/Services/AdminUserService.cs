using AutoMapper;
using beSQLSugar.Application.DTO.request;
using beSQLSugar.Application.DTO.response;
using beSQLSugar.Application.DTOs.request;
using beSQLSugar.Application.ServiceInterfaces;
using beSQLSugar.Domain.Enities;
using beSQLSugar.Domain.RepositoryInterfaces;

namespace beSQLSugar.Application.Services
{
    // Triển khai các phương thức trong IAdminUserService
    public class AdminUserService : IAdminUserService
    {
        // Inject repository và mapper
        private readonly IAdminUserRepository _repository;
        private readonly IMapper _mapper;

        // Khởi tạo với repository và mapper
        public AdminUserService(IAdminUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // Tạo mới người dùng
        public async Task<AdminUserResponse> CreateAsync(AdminUserRequest request)
        {
            // Kiểm tra username đã tồn tại chưa
            var existing = await _repository.GetByUsernameAsync(request.Username);
            if (existing != null) throw new Exception("Username already exists");

            // Map DTO sang entity
            var user = _mapper.Map<AdminUser>(request);
            // Bảo mật mật khẩu bằng hashing
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Lưu entity và trả về DTO response
            await _repository.AddAsync(user);
            return _mapper.Map<AdminUserResponse>(user);
        }

        // Lấy người dùng theo id
        public async Task<AdminUserResponse?> GetByIdAsync(int id)
        {
            // Lấy entity từ repository
            var user = await _repository.GetByIdAsync(id);

            // Map sang DTO hoặc trả về null nếu không tìm thấy
            return user == null ? null : _mapper.Map<AdminUserResponse>(user);
        }

        // Lấy tất cả người dùng
        public async Task<List<AdminUserResponse>> GetAllAsync()
        {
            // Lấy danh sách entity từ repository
            var users = await _repository.GetAllAsync();
            // Map sang danh sách DTO và trả về
            return _mapper.Map<List<AdminUserResponse>>(users);
        }

        // Cập nhật người dùng
        public async Task<AdminUserResponse?> UpdateAsync(int id, AdminUserRequest request)
        {
            // Lấy entity theo id
            var user = await _repository.GetByIdAsync(id);
            // Kiểm tra nếu user
            if (user == null) return null;

            // map các trường từ request sang entity
            _mapper.Map(request, user);

            // Nếu cập nhật mật khẩu thì hash mật khẩu mới
            if (!string.IsNullOrEmpty(request.Password))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Cập nhật entity vào db
            await _repository.UpdateAsync(user);
            // Trả về DTO response
            return _mapper.Map<AdminUserResponse>(user);
        }

        // Xóa người dùng
        public async Task<bool> DeleteAsync(int id)
        {
            // Lấy entity theo id
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return false;

            // Xóa entity nếu tìm thấy
            await _repository.DeleteAsync(id);
            return true;
        }

        // Tìm kiếm người dùng theo các tiêu chí trong request
        public async Task<List<AdminUserResponse>> SearchAsync(AdminUserSearchRequest request)
        {
            // Sử dụng repository để tìm kiếm entity theo tiêu chí
            var users = await _repository.SearchAsync(request);

            // Map danh sách entity sang DTO và trả về
            return _mapper.Map<List<AdminUserResponse>>(users);
        }
    }
}
