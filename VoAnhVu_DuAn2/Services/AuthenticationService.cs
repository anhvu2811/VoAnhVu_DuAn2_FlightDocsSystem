using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VoAnhVu_DuAn2.Entities;
using VoAnhVu_DuAn2.Models;

namespace VoAnhVu_DuAn2.Services
{
    public interface IAuthenticationService
    {
        string GenerateToken(UserEntity user, string roleName);
    }
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppSetting _appSettings;

        public AuthenticationService(IOptions<AppSetting> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string GenerateToken(UserEntity user, string roleName)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, roleName),
                new Claim("UserId", user.UserId),

                new Claim("TokenId", Guid.NewGuid().ToString())
            }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
