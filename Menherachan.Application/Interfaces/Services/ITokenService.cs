using Menherachan.Domain.Entities.DBOs;
using Menherachan.Domain.Entities.Responses;

namespace Menherachan.Application.Interfaces.Services
{
    public interface ITokenService
    {
        public string GenerateJwtToken(Admin admin);
        public RefreshToken GenerateRefreshToken();
    }
}