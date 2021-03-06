using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Menherachan.Application.CQRS.Queries.Board;
using Menherachan.Application.Interfaces;
using Menherachan.Application.Interfaces.Repositories;
using Menherachan.Domain.Entities.DBOs;
using Menherachan.Domain.Entities.Responses;
using Menherachan.Domain.Entities.ViewModels;
using Menherachan.Domain.Entities.ViewModels.Common;

namespace Menherachan.Application.CQRS.Handlers.BoardHandlers
{
    public class GetBoardHeaderHandler : IRequestHandler<GetBoardHeaderQuery, Response<BoardHeaderViewModel>>
    {
        private IBoardRepository _boardRepository;

        public GetBoardHeaderHandler(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }
        
        public async Task<Response<BoardHeaderViewModel>> Handle(GetBoardHeaderQuery request, CancellationToken cancellationToken)
        {
            var board = await _boardRepository.GetBoardAsync(request.Prefix);

            if (IsBad(board))
            {
                return new Response<BoardHeaderViewModel>("No board found");
            }

            var viewModel = new BoardHeaderViewModel(
                board.Prefix,
                board.Postfix,
                board.Title,
                board.Description,
                "На башорг!");
            
            return new Response<BoardHeaderViewModel>(viewModel);
        }

        private bool IsBad(Board board)
        {
            return
                string.IsNullOrEmpty(board.Prefix) || string.IsNullOrWhiteSpace(board.Prefix)
                &&
                string.IsNullOrEmpty(board.Postfix) || string.IsNullOrWhiteSpace(board.Postfix);
        }
    }
}