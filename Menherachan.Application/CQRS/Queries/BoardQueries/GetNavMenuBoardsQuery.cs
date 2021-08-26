using System.Collections.Generic;
using MediatR;
using Menherachan.Domain.Entities.Responses;
using Menherachan.Domain.Entities.ViewModels.Common;

namespace Menherachan.Application.CQRS.Queries.BoardQueries
{
    public record GetNavMenuBoardsQuery : IRequest<Response<IEnumerable<NavMenuBoardViewModel>>>;

}