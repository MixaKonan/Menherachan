using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Menherachan.Application.Interfaces.Repositories;
using Menherachan.Domain.Database;
using Menherachan.Domain.Entities.DBOs;
using Microsoft.EntityFrameworkCore;

namespace Menherachan.Infrastructure.Persistence.Repositories
{
    public class AdminRepository : BaseRepository, IAdminRepository
    {
        private DbSet<Admin> _admins;
        
        public AdminRepository(ApplicationDbContext context) : base(context)
        {
            _admins = context.Admins;
        }

        public async Task<IEnumerable<Admin>> GetData()
        {
            return await _admins
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Admin>> GetDataWithCondition(Expression<Func<Admin, bool>> condition)
        {
            return await _admins
                .Where(condition)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Admin>> GetDataWithIncluded()
        {
            return await _admins
                .Include(a => a.Ban)
                .Include(a => a.Post)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Admin>> GetDataWithConditionAndIncluded(Expression<Func<Admin, bool>> condition)
        {
            return await _admins
                .Where(condition)
                .Include(a => a.Ban)
                .Include(a => a.Post)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Admin> Find(Expression<Func<Admin, bool>> condition)
        {
            return await _admins.FirstAsync(condition);
        }

        public async Task<bool> ThereIsAdmin(Expression<Func<Admin, bool>> condition)
        {
            return await _admins.AnyAsync(condition);
        }
    }
}