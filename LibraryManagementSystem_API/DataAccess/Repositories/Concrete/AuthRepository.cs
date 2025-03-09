using LibraryManagementSystem_API.DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem_API.DataAccess.Entities;
using LibraryManagementSystem_API.Business.Dtos.AuthDtos;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;

namespace LibraryManagementSystem_API.DataAccess.Repositories.Concrete
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration configuration;

        public AuthRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string CreateAccessToken(UserEntity user) {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserName", user.UserName),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("Id", user.UserId.ToString()),
                    new Claim("TokenId", Guid.NewGuid().ToString())
                }),

                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);

            
            return jwtTokenHandler.WriteToken(token);
        }

        public string CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
