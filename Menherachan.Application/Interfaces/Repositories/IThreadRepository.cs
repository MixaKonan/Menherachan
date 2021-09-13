using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Menherachan.Domain.Entities.DBOs;

namespace Menherachan.Application.Interfaces.Repositories
{
    public interface IThreadRepository : IPagedRepository<Thread>
    {
        public Task<Thread> GetThreadWithPosts(int threadId);
        public Task<Thread> GetThreadWithPagedPosts(int threadId, int page, int pageSize);
    }
}