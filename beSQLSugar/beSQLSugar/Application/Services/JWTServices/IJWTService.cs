using beSQLSugar.Infrastructure.Database.Enities;

namespace beSQLSugar.Application.Services.JWT
{
    public interface IJWTService
    {
        (string Token, long ExpiresInSeconds) GenerateToken(AdminUser user);
    }
}
