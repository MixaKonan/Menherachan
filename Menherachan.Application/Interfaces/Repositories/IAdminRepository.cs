using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Menherachan.Domain.Entities.DBOs;

namespace Menherachan.Application.Interfaces.Repositories
{
    public interface IAdminRepository : IRepository<Admin>
    {
        public Task<Admin> FindAsync(Expression<Func<Admin, bool>> condition);
        public Task<bool> ThereIsAdminAsync(Expression<Func<Admin, bool>> condition);
    }
}