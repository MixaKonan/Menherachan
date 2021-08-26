using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Menherachan.Application.CQRS.Queries.BoardQueries;
using Menherachan.Application.Interfaces;
using Menherachan.Domain.Entities.DBOs;
using Menherachan.Domain.Entities.Responses;
using Thread = Menherachan.Domain.Entities.DBOs.Thread;

namespace Menherachan.Application.CQRS.Handlers.BoardHandlers
{
    public class GetThreadsPreviewHandler : IRequestHandler<GetThreadsPreviewsQuery, Response<IEnumerable<Post>>>
    {
        private IThreadRepository _threadRepository;

        public GetThreadsPreviewHandler(IThreadRepository threadRepository)
        {
            _threadRepository = threadRepository;
        }

        public async Task<Response<IEnumerable<Post>>> Handle(GetThreadsPreviewsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Thread> threads;

            if (request.Page != 0 && request.PageSize != 0)
            {
                threads = await _threadRepository.GetPagedThreadsWithConditionAndIncluded(
                    t => t.Board.Prefix == request.Prefix,
                    request.Page,
                    request.PageSize);
            }
            else
            {
                threads = await _threadRepository.GetDataWithConditionAndIncluded(
                    t => t.Board.Prefix == request.Prefix);
            }

            threads = threads.OrderByDescending(t => t.BumpInUnixTime);
            
            var data = new List<Post>();
            
            foreach (var thread in threads)
            {
                var postCount = thread.Post.Count;
                
                if (postCount > 0)
                {
                    var posts = thread.Post.OrderByDescending(p => p.CreatedAt);
                    
                    if (postCount <= 3)
                    {
                        data.AddRange(posts.Take(1));
                    }

                    if (postCount > 3)
                    {
                        data.AddRange(posts.Take(3));
                    }
                }
            }

            return new Response<IEnumerable<Post>>(data);
        }
    }
}