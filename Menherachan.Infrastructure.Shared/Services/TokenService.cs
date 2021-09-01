using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Menherachan.Application.Interfaces.Services;
using Menherachan.Domain.Entities.DBOs;
using Menherachan.Domain.Entities.Responses;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Menherachan.Infrastructure.Shared.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(Admin admin)
        {
            var nowDate = DateTime.Now;
            var expireDate = nowDate.AddDays(7);
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, admin.Email),
                new(ClaimTypes.Name, admin.Login),
                new(JwtRegisteredClaimNames.Exp, expireDate.ToString(CultureInfo.InvariantCulture))
            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                nowDate,
                expireDate,
                credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomString(16),
                ExpiresAt = DateTime.Now.AddDays(7)
            };
        }
        
        private string RandomString(int size)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[size];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return BitConverter.ToString(randomBytes).Replace("-", "");
            }
        }
    }
}