using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Menherachan.Application.Interfaces;
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

        public async Task<IList<Board>> GetData()
        {
            return await _boards
                .ToListAsync();
        }

        public async Task<IList<Board>> GetDataWithCondition(Expression<Func<Board, bool>> condition)
        {
            return await _boards
                .Where(condition)
                .ToListAsync();
        }

        public async Task<IList<Board>> GetDataWithIncluded()
        {
            return await _boards
                .Include(b => b.File)
                .Include(b => b.Post)
                .Include(b => b.Report)
                .Include(b => b.Thread)
                .ToListAsync();
        }

        public async Task<IList<Board>> GetDataWithConditionAndIncluded(Expression<Func<Board, bool>> condition)
        {
            return await _boards
                .Where(condition)
                .Include(b => b.File)
                .Include(b => b.Post)
                .Include(b => b.Report)
                .Include(b => b.Thread)
                .ToListAsync();
        }

        public async Task<IList<Board>> GetDataWithIncluded(string[] includes)
        {
            IQueryable<Board> query = _boards;

            query = GetIncludes(query, includes);

            return await query.Select(b => b).ToListAsync();
        }

        public async Task<IList<Board>> GetDataWithConditionAndIncluded(Expression<Func<Board, bool>> condition, string[] includes)
        {
            IQueryable<Board> query = _boards.Where(condition);

            query = GetIncludes(query, includes);
            
            return await query.ToListAsync();
        }

        private IQueryable<Board> GetIncludes(IQueryable<Board> query, string[] includes)
        {
            foreach (var include in includes)
            {
                switch (include)
                {
                    case "file":
                        query.Include(b => b.File);
                        break;
                    case "post":
                        query.Include(b => b.Post);
                        break;
                    case "thread":
                        query.Include(b => b.Thread);
                        break;
                    case "report":
                        query.Include(b => b.Report);
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
            
            return query;
        }
    }
}