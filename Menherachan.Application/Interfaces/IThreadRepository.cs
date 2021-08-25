using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Menherachan.Domain.Entities.DBOs;

namespace Menherachan.Application.Interfaces
{
    public interface IThreadRepository : IRepository<Thread>
    {
        public Task<IEnumerable<Thread>> GetPagedThreadsWithIncludes(int page, int pageSize);
        public Task<IEnumerable<Thread>> GetPagedThreadsWithCondition(Expression<Func<Thread, bool>> condition, int page, int pageSize);
        public Task<IEnumerable<Thread>> GetPagedThreadsWithConditionAndIncludes(Expression<Func<Thread, bool>> condition, int page, int pageSize);
    }
}