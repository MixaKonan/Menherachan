using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Menherachan.Application.Interfaces
{
    public interface IRepository<T>
    {
        public Task<IList<T>> GetData();
        
        public Task<IList<T>> GetDataWithCondition(Expression<Func<T, bool>> condition);
        
        public Task<IList<T>> GetDataWithIncluded();
        
        public Task<IList<T>> GetDataWithConditionAndIncluded(Expression<Func<T, bool>> condition);

        public Task<IList<T>> GetDataWithIncluded(string[] includes);
        public Task<IList<T>> GetDataWithConditionAndIncluded(Expression<Func<T, bool>> condition, string[] includes);
    }
}