using Menherachan.Domain.Entities.DBOs;
using System.Collections.Generic;
using MediatR;

namespace Menherachan.Application.CQRS.Queries.BoardQueries
{
    public record GetAllBoardsQuery : IRequest<IList<Board>>;

}