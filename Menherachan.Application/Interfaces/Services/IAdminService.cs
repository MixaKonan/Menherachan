using System.Threading.Tasks;
using Menherachan.Domain.Entities.Responses;

namespace Menherachan.Application.Interfaces.Services
{
    public interface IAdminService
    {
        public Task<AuthenticationResponse> AuthenticateAsync(string username, string password);
    }
}