using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Menherachan.Application.Interfaces.Repositories
{
    public interface IPagedRepository<T> : IRepository<T>
    {
        public Task<IEnumerable<T>> GetPagedThreadsWithIncludedAsync(int page, int pageSize);
        public Task<IEnumerable<T>> GetPagedThreadsWithConditionAsync(Expression<Func<T, bool>> condition, int page, int pageSize);
        public Task<IEnumerable<T>> GetPagedThreadsWithConditionAndIncludedAsync(Expression<Func<T, bool>> condition, int page, int pageSize);
    }
}