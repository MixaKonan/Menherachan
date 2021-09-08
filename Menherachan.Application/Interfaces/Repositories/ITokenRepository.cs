using System.Threading.Tasks;
using Menherachan.Domain.Entities.DBOs;

namespace Menherachan.Application.Interfaces.Repositories
{
    public interface ITokenRepository : IRepository<Token>
    {
        public Task<Token> GetToken(string token);
        public Task<Token> GetTokenWithIncluded(string token);
    }
}