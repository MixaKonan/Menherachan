using System.Threading.Tasks;
using Menherachan.Application.Exceptions;
using Menherachan.Application.Interfaces.Repositories;
using Menherachan.Application.Interfaces.Services;
using Menherachan.Domain.Entities.DBOs;
using Menherachan.Domain.Entities.Responses;

namespace Menherachan.Infrastructure.Shared.Services
{
    public class AdminService : IAdminService
    {
        private readonly  IAdminRepository _adminRepository;
        private readonly ITokenService _tokenService;

        public AdminService(IAdminRepository adminRepository, ITokenService tokenService)
        {
            _adminRepository = adminRepository;
            _tokenService = tokenService;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(string username, string password)
        {
            var admin = await IsValidCredentialsAsync(username, password);
            
            if (admin is null)
            {
                throw new ApiException($"No admins found with {username} credentials.");
            }

            var token = _tokenService.GenerateJwtToken(admin);

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
    }
}