using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Menherachan.Application.CQRS.Queries.BoardQueries;
using Menherachan.Application.Interfaces;
using Menherachan.Domain.Entities.Responses;
using Menherachan.Domain.Entities.ViewModels.Common;
using Thread = Menherachan.Domain.Entities.DBOs.Thread;

namespace Menherachan.Application.CQRS.Handlers.BoardHandlers
{
    public class GetThreadsPreviewHandler : IRequestHandler<GetThreadsPreviewsQuery, Response<IEnumerable<PostViewModel>>>
    {
        private IThreadRepository _threadRepository;
        private IMapper _mapper;

        public GetThreadsPreviewHandler(IThreadRepository threadRepository, IMapper mapper)
        {
            _threadRepository = threadRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<PostViewModel>>> Handle(GetThreadsPreviewsQuery request, CancellationToken cancellationToken)
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
            
            var data = new List<PostViewModel>();
            
            foreach (var thread in threads)
            {
                var postCount = thread.Post.Count;
                
                if (postCount > 0)
                {
                    var posts = thread.Post.OrderByDescending(p => p.CreatedAt);
                    
                    if (postCount <= 3)
                    {
                        foreach (var post in posts.Take(1))
                        {
                            var model = _mapper.Map<PostViewModel>(post);
                            
                            data.Add(model);
                        }
                    }

                    if (postCount > 3)
                    {
                        foreach (var post in posts.Take(3))
                        {
                            var model = _mapper.Map<PostViewModel>(post);

                            data.Add(model);
                        }
                    }
                }
            }

            return new Response<IEnumerable<PostViewModel>>(data);
        }
    }
}