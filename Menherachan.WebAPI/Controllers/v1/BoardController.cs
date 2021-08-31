using System.Threading.Tasks;
using MediatR;
using Menherachan.Application.CQRS.Queries.Board;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Menherachan.WebAPI.Controllers.v1
{
    [ApiController]
    [Route("api/v{version}/board/")]
    public class BoardController : Controller
    {
        private IMediator _mediator;

        public BoardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("nav-menu")]
        public async Task<IActionResult> GetNavMenuBoards()
        {
            var query = new GetNavMenuBoardsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        
        [HttpGet("short-info")]
        public async Task<IActionResult> GetBoardsShortInfo()
        {
            var query = new GetBoardsShortInfoQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("header")]
        public async Task<IActionResult> GetBoard([FromQuery] GetBoardHeaderQuery headerQuery)
        {
            var result = await _mediator.Send(headerQuery);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("preview")]
        public async Task<IActionResult> GetPreview([FromQuery] GetThreadsPreviewsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}