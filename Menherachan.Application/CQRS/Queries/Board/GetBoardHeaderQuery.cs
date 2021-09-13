using System.Collections.Generic;
using MediatR;
using Menherachan.Domain.Entities.Responses;
using Menherachan.Domain.Entities.ViewModels.Common;

namespace Menherachan.Application.CQRS.Queries.Board
{
    public record GetBoardHeaderQuery(string Prefix) : IRequest<Response<BoardHeaderViewModel>>;
}