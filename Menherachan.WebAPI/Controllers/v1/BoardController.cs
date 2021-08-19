using System.Threading.Tasks;
using MediatR;
using Menherachan.Application.CQRS.Queries.BoardQueries;
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

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBoards()
        {
            var query = new GetAllBoardsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        
        [HttpGet("all/including")]
        public async Task<IActionResult> GetAllBoards([FromQuery] GetAllBoardsWithIncludesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}