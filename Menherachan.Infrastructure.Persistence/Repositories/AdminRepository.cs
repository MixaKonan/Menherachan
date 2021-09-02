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

        public async Task DeleteAsync(Admin entity)
        {
            _admins.Remove(entity);
            await this.Context.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<Admin> entities)
        {
            _admins.RemoveRange(entities);
            await this.Context.SaveChangesAsync();
        }

        public async Task AddAsync(Admin entity)
        {
            await _admins.AddAsync(entity);
            await this.Context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<Admin> entities)
        {
            await _admins.AddRangeAsync(entities);
            await this.Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Admin>> GetDataAsync()
        {
            return await _admins
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Admin>> GetDataWithConditionAsync(Expression<Func<Admin, bool>> condition)
        {
            return await _admins
                .Where(condition)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Admin>> GetDataWithIncludedAsync()
        {
            return await _admins
                .Include(a => a.Ban)
                .Include(a => a.Post)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Admin>> GetDataWithConditionAndIncludedAsync(Expression<Func<Admin, bool>> condition)
        {
            return await _admins
                .Where(condition)
                .Include(a => a.Ban)
                .Include(a => a.Post)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Admin> FindAsync(Expression<Func<Admin, bool>> condition)
        {
            try
            {
                return await _admins.FirstAsync(condition);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        public async Task<bool> ThereIsAdminAsync(Expression<Func<Admin, bool>> condition)
        {
            return await _admins.AnyAsync(condition);
        }
    }
}