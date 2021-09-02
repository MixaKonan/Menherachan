using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Menherachan.Application.Interfaces;
using Menherachan.Application.Interfaces.Repositories;
using Menherachan.Domain.Database;
using Menherachan.Domain.Entities.DBOs;
using Microsoft.EntityFrameworkCore;

namespace Menherachan.Infrastructure.Persistence.Repositories
{
    public class BoardRepository : BaseRepository, IBoardRepository
    {
        private DbSet<Board> _boards;

        public BoardRepository(ApplicationDbContext context) : base(context)
        {
            _boards = context.Boards;
        }

        public async Task<IEnumerable<Board>> GetDataAsync()
        {
            return await _boards
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Board>> GetDataWithConditionAsync(Expression<Func<Board, bool>> condition)
        {
            return await _boards
                .Where(condition)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Board> GetBoardAsync(string prefix)
        {
            try
            {
                return await _boards.FirstAsync(board => board.Prefix == prefix);
            }
            catch (InvalidOperationException)
            {
                return new Board();
            }
        }

        public async Task<Board> GetBoardAsync(Expression<Func<Board, bool>> condition)
        {
            try
            {
                return await _boards.FirstAsync(condition);
            }
            catch (InvalidOperationException)
            {
                return new Board();
            }
        }

        public async Task<IEnumerable<Board>> GetDataWithIncludedAsync()
        {
            return await _boards
                .Include(b => b.Report)
                .Include(b => b.Thread)
                .Include(b => b.Post)
                .Include(b => b.File)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Board>> GetDataWithConditionAndIncludedAsync(Expression<Func<Board, bool>> condition)
        {
            return await _boards
                .Where(condition)
                .Include(b => b.Thread)
                .Include(b => b.Post)
                .Include(b => b.File)
                .Include(b => b.Report)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task DeleteAsync(Board entity)
        {
            _boards.Remove(entity);
            await this.Context.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<Board> entities)
        {
            _boards.RemoveRange(entities);
            await this.Context.SaveChangesAsync();
        }

        public async Task AddAsync(Board entity)
        {
            await _boards.AddAsync(entity);
            await this.Context.SaveChangesAsync();
        }

        public Task AddRangeAsync(IEnumerable<Board> entities)
        {
            throw new NotImplementedException();
        }
    }
}