using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using BetterCinema.Domain.Entities;

namespace BetterCinema.Api.Cryptography
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(UserEntity userEntity);
    }
    public class JwtTokenGenerator(IConfiguration configuration, SecurityTokenHandler securityTokenHandler)
        : IJwtTokenGenerator
    {
        public string GenerateToken(UserEntity userEntity)
        {
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("Security:Secret").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("UserId", userEntity.Id.ToString()),
                    new Claim(ClaimTypes.Role, userEntity.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = securityTokenHandler.CreateToken(tokenDescriptor);

            var tokenString = securityTokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
