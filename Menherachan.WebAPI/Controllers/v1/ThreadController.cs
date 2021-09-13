using System.Threading.Tasks;
using MediatR;
using Menherachan.Application.CQRS.Queries.Thread;
using Microsoft.AspNetCore.Mvc;

namespace Menherachan.WebAPI.Controllers.v1
{
    [ApiController]
    [Route("api/v{version}/thread/")]
    public class ThreadController : Controller
    {
        private readonly IMediator _mediator;

        public ThreadController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("posts")]
        public async Task<IActionResult> Index([FromQuery] GetThreadPostsQuery query)
        {
            var result = await _mediator.Send(query);
            
            return Ok(result);
        }
    }
}