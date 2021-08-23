using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Menherachan.Application.CQRS.Queries.BoardQueries;
using Menherachan.Application.Interfaces;
using Menherachan.Domain.Entities.Responses;
using Menherachan.Domain.Entities.ViewModels;

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
            if (request.IsMainPage)
            {
                var result = new BoardHeaderViewModel("bash", "org", "Menherachan", "Добро пожаловать", "На сосач");

                return new Response<BoardHeaderViewModel>(result);
            }
            
            var board = await _boardRepository.GetBoard(request.Prefix);

            var viewModel = new BoardHeaderViewModel(
                board.Prefix,
                board.Postfix,
                board.Title,
                board.Description,
                "На башорг!");

            return new Response<BoardHeaderViewModel>(viewModel);
        }
    }
}