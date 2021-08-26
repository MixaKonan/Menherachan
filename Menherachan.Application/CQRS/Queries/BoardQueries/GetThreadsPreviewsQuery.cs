using System.Collections.Generic;
using MediatR;
using Menherachan.Domain.Entities.Responses;
using Menherachan.Domain.Entities.ViewModels.Common;

namespace Menherachan.Application.CQRS.Queries.BoardQueries
{
    public class GetThreadsPreviewsQuery : IRequest<Response<IEnumerable<PostViewModel>>>
    {
        public string Prefix { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public GetThreadsPreviewsQuery()
        {
            
        }

        public GetThreadsPreviewsQuery(string prefix, int page, int pageSize)
        {
            Prefix = prefix;
            Page = page;
            PageSize = pageSize;
        }
    }
}