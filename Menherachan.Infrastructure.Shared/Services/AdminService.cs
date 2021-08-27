using System.Threading.Tasks;
using Menherachan.Application.Interfaces.Repositories;
using Menherachan.Application.Interfaces.Services;
using Menherachan.Domain.Entities.DBOs;

namespace Menherachan.Infrastructure.Shared.Services
{
    public class AdminService : IAdminService
    {
        private IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<bool> TryFindAdmin(string email, string password, out Admin admin)
        {
            admin = await _adminRepository.Find(a => a.Email == email && a.PasswordHash == password);
            return admin is null;
        }
    }
}