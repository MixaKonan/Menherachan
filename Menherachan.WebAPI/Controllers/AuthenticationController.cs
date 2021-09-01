using System.Threading.Tasks;
using MediatR;
using Menherachan.Application.CQRS.Commands.Authentication;
using Menherachan.WebAPI.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Menherachan.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v{version}/authentication/")]
    public class AuthenticationController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ICookieService _cookieService;

        public AuthenticationController(IMediator mediator, ICookieService cookieService)
        {
            _mediator = mediator;
            _cookieService = cookieService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest request)
        {
            var response = await _mediator.Send(request);
            
            _cookieService.SetTokenCookie(this.Response, response.Data.Token);
            
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        [Route("logout")]
        public IActionResult Logout()
        {
            _cookieService.RemoveTokenCookie(this.Response);

            return Ok("Successfully logged out.");
        }
    }
}