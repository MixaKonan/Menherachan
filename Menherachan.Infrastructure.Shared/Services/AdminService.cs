using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Menherachan.Application.Exceptions;
using Menherachan.Application.Interfaces.Repositories;
using Menherachan.Application.Interfaces.Services;
using Menherachan.Domain.Entities.DBOs;
using Menherachan.Domain.Entities.Responses;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Menherachan.Infrastructure.Shared.Services
{
    public class AdminService : IAdminService
    {
        private IAdminRepository _adminRepository;
        private IConfiguration _configuration;

        public AdminService(IAdminRepository adminRepository, IConfiguration configuration)
        {
            _adminRepository = adminRepository;
            _configuration = configuration;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(string username, string password)
        {
            var admin = await IsValidCredentialsAsync(username, password);
            
            if (admin is null)
            {
                throw new ApiException($"No admins found with {username} credentials.");
            }

            var token = GenerateToken(admin);

            return new AuthenticationResponse
            {
                Email = admin.Email,
                Nickname = admin.Login,
                Token = token
            };
        }

        private async Task<Admin> IsValidCredentialsAsync(string username, string password)
        {
            var hash = await HashingService.GetHashFromStringAsync(password);
            return await _adminRepository.FindAsync(a => a.Email == username && a.PasswordHash == hash);
        }

        private string GenerateToken(Admin admin)
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
    }
}