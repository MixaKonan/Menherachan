using System.Collections.Generic;
using MediatR;
using Menherachan.Domain.Entities.Responses;
using Menherachan.Domain.Entities.ViewModels.Thread;

namespace Menherachan.Application.CQRS.Queries.Thread
{
    public class GetThreadPostsQuery : IRequest<Response<ThreadViewModel>>
    {
        public int ThreadId { get; set; }
    }
}