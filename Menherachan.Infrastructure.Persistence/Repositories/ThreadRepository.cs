using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Menherachan.Application.Interfaces;
using Menherachan.Application.Interfaces.Repositories;
using Menherachan.Domain.Database;
using Microsoft.EntityFrameworkCore;
using Thread = Menherachan.Domain.Entities.DBOs.Thread;

namespace Menherachan.Infrastructure.Persistence.Repositories
{
    public class ThreadRepository : BaseRepository, IThreadRepository
    {
        private DbSet<Thread> _threads;
        
        public ThreadRepository(ApplicationDbContext context) : base(context)
        {
            _threads = context.Threads;
        }

        public async Task<IEnumerable<Thread>> GetDataAsync()
        {
            return await _threads
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Thread>> GetDataWithConditionAsync(Expression<Func<Thread, bool>> condition)
        {
            return await _threads
                .Where(condition)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Thread>> GetDataWithIncludedAsync()
        {
            return await _threads
                .Include(t => t.Board)
                .Include(t => t.Post)
                .Include(t => t.File)
                .Include(t => t.Report)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Thread>> GetDataWithConditionAndIncludedAsync(Expression<Func<Thread, bool>> condition)
        {
            return await _threads
                .Where(condition)
                .Include(t => t.Board)
                .Include(t => t.Post)
                .ThenInclude(p => p.Admin)
                .Include(t => t.Post)
                .ThenInclude(p => p.File)
                .Include(t => t.Report)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Thread>> GetPagedThreadsWithIncludedAsync(int page, int pageSize)
        {
            return await _threads
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(t => t.Board)
                .Include(t => t.Post)
                .Include(t => t.File)
                .Include(t => t.Report)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Thread>> GetPagedThreadsWithConditionAsync(Expression<Func<Thread, bool>> condition, int page, int pageSize)
        {
            return await _threads
                .Where(condition)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Thread>> GetPagedThreadsWithConditionAndIncludedAsync(Expression<Func<Thread, bool>> condition, int page, int pageSize)
        {
            return await _threads
                .Where(condition)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(t => t.Board)
                .Include(t => t.Post)
                .Include(t => t.File)
                .Include(t => t.Report)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}