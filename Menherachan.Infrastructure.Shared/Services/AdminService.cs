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
            var admin = await GetAdminByCredentialsAsync(username, password);
            
            if (admin is null)
            {
                throw new ApiException($"No admins found with {username} credentials.");
            }

            var jwtToken = _tokenService.GenerateJwtToken(admin);
            var refreshToken = _tokenService.GenerateRefreshToken();
            var token = _mapper.Map<Token>(refreshToken);
            token.Admin = admin;
            
            await _tokenRepository.AddAsync(token);

            var authResponse = new AuthenticationResponse
            {
                Email = admin.Email,
                Nickname = admin.Login,
                Bearer = jwtToken
            };
            
            return new Tuple<AuthenticationResponse, RefreshToken>(authResponse, refreshToken);
        }

        public async Task<Tuple<AuthenticationResponse, RefreshToken>> RefreshAdminToken(string token)
        {
            var tkn = await _tokenRepository.GetTokenWithIncluded(token);

            if (tkn.ExpiresAt < DateTime.Now)
            {
                throw new ApiException("Token has expired.");
            }

            var refreshToken = _mapper.Map<RefreshToken>(tkn);
            
            var response = new AuthenticationResponse
            {
                Email = tkn.Admin.Email,
                Nickname = tkn.Admin.Login,
                Bearer = _tokenService.GenerateJwtToken(tkn.Admin)
            };
            
            return new Tuple<AuthenticationResponse, RefreshToken>(response, refreshToken);
        }

        private async Task<Admin> GetAdminByCredentialsAsync(string username, string password)
        {
            var hash = await HashingService.GetHashFromStringAsync(password);
            return await _adminRepository.FindAsync(a => a.Email == username && a.PasswordHash == hash);
        }
    }
}