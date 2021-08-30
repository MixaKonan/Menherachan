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
using Microsoft.IdentityModel.Tokens;

namespace Menherachan.Infrastructure.Shared.Services
{
    public class AdminService : IAdminService
    {
        private IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
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
            return await _adminRepository.FindAsync(a => a.Email == username && a.PasswordHash == password);
        }

        private JwtSecurityToken GenerateToken(Admin admin)
        {
            var expireDate = new DateTimeOffset(DateTime.Now.AddDays(7)).ToUnixTimeSeconds();
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, admin.Email),
                new(ClaimTypes.Name, admin.Login),
                new(JwtRegisteredClaimNames.Exp, expireDate.ToString())
            };
            
            //TODO: Change secret key
            var token = new JwtSecurityToken(
                new JwtHeader(
                    new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MyVerySecretKeyWhichIWillChangeLater")),
                        SecurityAlgorithms.Sha256)),
                new JwtPayload(claims));

            return token;
        }
    }
}