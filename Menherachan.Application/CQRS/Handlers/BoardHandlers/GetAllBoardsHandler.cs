using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Menherachan.Application.CQRS.Queries.BoardQueries;
using Menherachan.Application.Interfaces;
using Menherachan.Domain.Entities.Responses;
using Menherachan.Domain.Entities.ViewModels;

namespace Menherachan.Application.CQRS.Handlers.BoardHandlers
{
    public class GetAllBoardsHandler : IRequestHandler<GetNavMenuBoardsQuery, Response<IEnumerable<NavMenuBoardViewModel>>>
    {
        private IBoardRepository _boardRepository;

        public GetAllBoardsHandler(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        public async Task<Response<IEnumerable<NavMenuBoardViewModel>>> Handle(GetNavMenuBoardsQuery request, CancellationToken cancellationToken)
        {
            var boards = await _boardRepository.GetData();

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