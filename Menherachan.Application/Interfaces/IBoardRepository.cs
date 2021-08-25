using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Menherachan.Domain.Entities.DBOs;

namespace Menherachan.Application.Interfaces
{
    public interface IBoardRepository : IRepository<Board>
    {
        public Task<Board> GetBoard(string prefix);

        public Task<Board> GetBoard(Expression<Func<Board, bool>> condition);
    }
}