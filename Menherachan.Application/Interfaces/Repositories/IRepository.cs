using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Menherachan.Application.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        public Task<IEnumerable<T>> GetDataAsync();
        
        public Task<IEnumerable<T>> GetDataWithConditionAsync(Expression<Func<T, bool>> condition);
        
        public Task<IEnumerable<T>> GetDataWithIncludedAsync();
        
        public Task<IEnumerable<T>> GetDataWithConditionAndIncludedAsync(Expression<Func<T, bool>> condition);
    }
}