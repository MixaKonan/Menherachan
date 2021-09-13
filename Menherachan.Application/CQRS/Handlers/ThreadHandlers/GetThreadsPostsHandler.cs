using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Menherachan.Application.CQRS.Queries.Thread;
using Menherachan.Application.Interfaces.Repositories;
using Menherachan.Domain.Entities.Responses;
using Menherachan.Domain.Entities.ViewModels.Thread;

namespace Menherachan.Application.CQRS.Handlers.ThreadHandlers
{
    public class GetThreadsPostsHandler : IRequestHandler<GetThreadPostsQuery, Response<ThreadViewModel>>
    {
        private readonly IThreadRepository _threadRepository;
        private readonly IMapper _mapper;

        public GetThreadsPostsHandler(IMapper mapper, IThreadRepository threadRepository)
        {
            _mapper = mapper;
            _threadRepository = threadRepository;
        }

        public async Task<Response<ThreadViewModel>> Handle(GetThreadPostsQuery request, CancellationToken cancellationToken)
        {
            var thread = await _threadRepository.GetThreadWithPosts(request.ThreadId);

            var tvm = _mapper.Map<ThreadViewModel>(thread);
            
            return new Response<ThreadViewModel>(tvm);
        }
    }
}