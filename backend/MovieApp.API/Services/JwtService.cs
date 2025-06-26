// Services/JwtService.cs
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MovieApp.API.Services
{
    public class JwtService
    {
        private readonly string _secret;

        public JwtService(IConfiguration configuration)
        {
            _secret = configuration["JWT_SECRET"];

            if (string.IsNullOrWhiteSpace(_secret))
            {
                throw new Exception("JWT_SECRET no fue encontrado en el IConfiguration.");
            }
        }

        public string GenerateToken(int userId, string email)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(4),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
