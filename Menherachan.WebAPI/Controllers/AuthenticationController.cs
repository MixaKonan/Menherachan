using System.Threading.Tasks;
using MediatR;
using Menherachan.Application.CQRS.Commands.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Menherachan.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v{version}/authentication/")]
    public class AuthenticationController : Controller
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest request)
        {
            var response = await _mediator.Send(request);
            
            return Ok(response);
        }
        
        [HttpPost]
        [Route("logout")]
        public IActionResult Logout()
        {
            return Ok("Logout.");
        }
    }
}