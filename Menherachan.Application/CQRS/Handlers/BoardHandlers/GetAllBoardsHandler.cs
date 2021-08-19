using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Menherachan.Application.CQRS.Queries.BoardQueries;
using Menherachan.Application.Interfaces;
using Menherachan.Domain.Entities.DBOs;

namespace Menherachan.Application.CQRS.Handlers.BoardHandlers
{
    public class GetAllBoardsHandler : IRequestHandler<GetAllBoardsQuery, IList<Board>>
    {
        private IBoardRepository _boardRepository;

        public GetAllBoardsHandler(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        public async Task<IList<Board>> Handle(GetAllBoardsQuery request, CancellationToken cancellationToken)
        {
            return await _boardRepository.GetDataWithIncluded();
        }
    }
}