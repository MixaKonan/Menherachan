using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Menherachan.Application.CQRS.Queries.Board;
using Menherachan.Application.Interfaces;
using Menherachan.Application.Interfaces.Repositories;
using Menherachan.Domain.Entities.Responses;
using Menherachan.Domain.Entities.ViewModels;
using Menherachan.Domain.Entities.ViewModels.Common;

namespace Menherachan.Application.CQRS.Handlers.BoardHandlers
{
    public class GetNavMenuHandler : IRequestHandler<GetNavMenuBoardsQuery, Response<IEnumerable<NavMenuBoardViewModel>>>
    {
        private IBoardRepository _boardRepository;

        public GetNavMenuHandler(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        public async Task<Response<IEnumerable<NavMenuBoardViewModel>>> Handle(GetNavMenuBoardsQuery request, CancellationToken cancellationToken)
        {
            var boards = await _boardRepository.GetDataWithConditionAsync(b => !b.IsHidden);

            var data = new List<NavMenuBoardViewModel>();

            foreach (var board in boards)
            {
                data.Add(new NavMenuBoardViewModel(
                    board.Prefix,
                    board.Postfix));
            }
            
            var response = new Response<IEnumerable<NavMenuBoardViewModel>>(data);

            return response;
        }
    }
}