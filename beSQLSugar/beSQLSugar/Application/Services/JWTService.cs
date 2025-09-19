using beSQLSugar.Application.ServiceInterfaces;
using beSQLSugar.Domain.Enities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace beSQLSugar.Application.Services
{
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _cfg;
        public JWTService(IConfiguration cfg)
        {
            _cfg = cfg;
        }

        public (string Token, long ExpiresInSeconds) GenerateToken(AdminUser user)
        {
            var key = _cfg["Jwt:Key"] ?? throw new Exception("Jwt:Key not configured");
            var issuer = _cfg["Jwt:Issuer"] ?? throw new Exception("Jwt:Issuer not configured");
            var audience = _cfg["Jwt:Audience"] ?? throw new Exception("Jwt:Audience not configured");
            var minutes = int.TryParse(_cfg["Jwt:ExpireMinutes"], out var m) ? m : 60;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim("uid", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(minutes);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: credentials
            );

            var tokenText = new JwtSecurityTokenHandler().WriteToken(token);
            return (tokenText, (long)(expires - now).TotalSeconds);
        }
    }
}
