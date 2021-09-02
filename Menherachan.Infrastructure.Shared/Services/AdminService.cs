using System;
using System.Threading.Tasks;
using AutoMapper;
using Menherachan.Application.Exceptions;
using Menherachan.Application.Interfaces.Repositories;
using Menherachan.Application.Interfaces.Services;
using Menherachan.Domain.Entities.DBOs;
using Menherachan.Domain.Entities.Responses;

namespace Menherachan.Infrastructure.Shared.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AdminService(IAdminRepository adminRepository, ITokenService tokenService, ITokenRepository tokenRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _tokenService = tokenService;
            _tokenRepository = tokenRepository;
            _mapper = mapper;
        }

        public async Task<Tuple<AuthenticationResponse, RefreshToken>> AuthenticateAsync(string username, string password)
        {
            var admin = await IsValidCredentialsAsync(username, password);
            
            if (admin is null)
            {
                throw new ApiException($"No admins found with {username} credentials.");
            }

            var jwtToken = _tokenService.GenerateJwtToken(admin);
            var refreshToken = _tokenService.GenerateRefreshToken();
            
            await _tokenRepository.AddAsync(_mapper.Map<Token>(refreshToken));

            var authResponse = new AuthenticationResponse
            {
                Email = admin.Email,
                Nickname = admin.Login,
                Token = jwtToken
            };
            
            return new Tuple<AuthenticationResponse, RefreshToken>(authResponse, refreshToken);
        }

        public async Task<RefreshToken> RefreshAdminToken()
        {
            var refreshToken = _tokenService.GenerateRefreshToken();

            await _tokenRepository.AddAsync(_mapper.Map<Token>(refreshToken));
            
            return refreshToken;
        }

        private async Task<Admin> IsValidCredentialsAsync(string username, string password)
        {
            var hash = await HashingService.GetHashFromStringAsync(password);
            return await _adminRepository.FindAsync(a => a.Email == username && a.PasswordHash == hash);
        }
    }
}