using AutoMapper;
using beSQLSugar.Application.DTO.request;
using beSQLSugar.Application.DTO.response;
using beSQLSugar.Application.ServiceInterfaces;
using beSQLSugar.Domain.Enities;
using beSQLSugar.Domain.RepositoryInterfaces;

namespace beSQLSugar.Application.Services
{
    public class AdminUserService : IAdminUserService
    {
        private readonly IAdminUserRepository _repository;
        private readonly IMapper _mapper;

        public AdminUserService(IAdminUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AdminUserResponse> CreateAsync(AdminUserRequest request)
        {
            var existing = await _repository.GetByUsernameAsync(request.Username);
            if (existing != null) throw new Exception("Username already exists");

            var user = _mapper.Map<AdminUser>(request);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            await _repository.AddAsync(user);
            return _mapper.Map<AdminUserResponse>(user);
        }

        public async Task<AdminUserResponse?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user == null ? null : _mapper.Map<AdminUserResponse>(user);
        }

        public async Task<List<AdminUserResponse>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();
            return _mapper.Map<List<AdminUserResponse>>(users);
        }

        public async Task<AdminUserResponse?> UpdateAsync(int id, AdminUserRequest request)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return null;

            _mapper.Map(request, user);

            if (!string.IsNullOrEmpty(request.Password))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            await _repository.UpdateAsync(user);
            return _mapper.Map<AdminUserResponse>(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }
    }
}
