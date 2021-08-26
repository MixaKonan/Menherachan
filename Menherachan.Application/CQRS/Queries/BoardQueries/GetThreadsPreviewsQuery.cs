using System.Collections.Generic;
using System.Linq;
using MediatR;
using Menherachan.Domain.Entities.DBOs;
using Menherachan.Domain.Entities.Responses;
using Menherachan.Domain.Entities.ViewModels;

namespace Menherachan.Application.CQRS.Queries.BoardQueries
{
    public record GetThreadsPreviewsQuery(string Prefix, int Page, int PageSize) : IRequest<Response<IEnumerable<Post>>>;
}