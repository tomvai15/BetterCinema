using BetterCinema.Api.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace BetterCinema.Api.TokenGeneration
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration configuration;
        private readonly SecurityTokenHandler securityTokenHandler;

        public JwtTokenGenerator(IConfiguration configuration, SecurityTokenHandler securityTokenHandler)
        {
            this.configuration = configuration;
            this.securityTokenHandler = securityTokenHandler;
        }

        public string GenerateToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("Security:Secret").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
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
