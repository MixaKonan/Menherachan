using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Menherachan.Application.Interfaces
{
    public interface IRepository<T>
    {
        public Task<IEnumerable<T>> GetData();
        
        public Task<IEnumerable<T>> GetDataWithCondition(Expression<Func<T, bool>> condition);
        
        public Task<IEnumerable<T>> GetDataWithIncluded();
        
        public Task<IEnumerable<T>> GetDataWithConditionAndIncluded(Expression<Func<T, bool>> condition);
    }
}