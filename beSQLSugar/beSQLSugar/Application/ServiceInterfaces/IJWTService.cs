using beSQLSugar.Domain.Enities;

namespace beSQLSugar.Application.ServiceInterfaces
{
    public interface IJWTService
    {
        (string Token, long ExpiresInSeconds) GenerateToken(AdminUser user);
    }
}
