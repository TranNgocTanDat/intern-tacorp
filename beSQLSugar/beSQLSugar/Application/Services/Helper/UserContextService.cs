using System.Security.Claims;

namespace beSQLSugar.Application.Services.Helper
{
    public class UserContextService : IUserContextService
    {
        private readonly ClaimsPrincipal _user;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _user = httpContextAccessor.HttpContext?.User 
            ?? throw new UnauthorizedAccessException("Không tìm thấy thông tin người dùng");
    }

    public int GetUserId()
    {
        var userIdClaim = _user.FindFirst("uid");
        if (userIdClaim == null)
            throw new UnauthorizedAccessException("Không tìm thấy userId trong token");

        return int.Parse(userIdClaim.Value);
    }

    public string GetUserName()
    {
        var userNameClaim = _user.FindFirst(ClaimTypes.Name);
        return userNameClaim?.Value ?? "Unknown";
    }
    }
}
