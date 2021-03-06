using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Menherachan.Application.CQRS.Queries.Board;
using Menherachan.Application.Interfaces;
using Menherachan.Application.Interfaces.Repositories;
using Menherachan.Domain.Entities.Responses;
using Menherachan.Domain.Entities.ViewModels;
using Menherachan.Domain.Entities.ViewModels.Main;

namespace Menherachan.Application.CQRS.Handlers.BoardHandlers
{
    public class GetBoardsShortInfoHandler : IRequestHandler<GetBoardsShortInfoQuery, Response<IEnumerable<MainPageBoardInfoViewModel>>>
    {
        private IBoardRepository _boardRepository;

        public GetBoardsShortInfoHandler(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        public async Task<Response<IEnumerable<MainPageBoardInfoViewModel>>> Handle(GetBoardsShortInfoQuery request, CancellationToken cancellationToken)
        {
            var result = await _boardRepository.GetDataWithConditionAndIncludedAsync(b => !b.IsHidden);

            var data = new List<MainPageBoardInfoViewModel>();

            foreach (var board in result)
            {
                data.Add(new MainPageBoardInfoViewModel(
                    board.Prefix,
                    board.Thread.Count,
                    board.Post.Count,
                    board.File.Count));
            }

            var response = new Response<IEnumerable<MainPageBoardInfoViewModel>>(data);

            return response;
        }
    }
}