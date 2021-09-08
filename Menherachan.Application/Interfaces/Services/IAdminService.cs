using System;
using System.Threading.Tasks;
using Menherachan.Domain.Entities.Responses;

namespace Menherachan.Application.Interfaces.Services
{
    public interface IAdminService
    {
        public Task<Tuple<AuthenticationResponse, RefreshToken>> AuthenticateAsync(string username, string password);
        public Task<Tuple<AuthenticationResponse, RefreshToken>> RefreshAdminToken(string token);
    }
}