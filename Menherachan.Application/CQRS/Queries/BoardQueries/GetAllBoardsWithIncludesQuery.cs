using System.Collections.Generic;
using MediatR;
using Menherachan.Domain.Entities.DBOs;

namespace Menherachan.Application.CQRS.Queries.BoardQueries
{
    public record GetAllBoardsWithIncludesQuery(string[] Includes) : IRequest<IList<Board>>;
}