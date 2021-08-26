using System.Collections.Generic;
using MediatR;
using Menherachan.Domain.Entities.Responses;
using Menherachan.Domain.Entities.ViewModels.Main;

namespace Menherachan.Application.CQRS.Queries.BoardQueries
{
    public record GetBoardsShortInfoQuery : IRequest<Response<IEnumerable<MainPageBoardInfoViewModel>>>;
}