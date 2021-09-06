using System.Threading.Tasks;
using MediatR;
using Menherachan.Application.CQRS.Commands.Authentication;
using Menherachan.Application.Interfaces.Services;
using Menherachan.WebAPI.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Menherachan.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v{version}/authentication/")]
    public class AuthenticationController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ICookieService _cookieService;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IMediator mediator,
            ICookieService cookieService,
            IConfiguration configuration)
        {
            _mediator = mediator;
            _cookieService = cookieService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest request)
        {
            var data = await _mediator.Send(request);
            var response = data.Data.Item1;
            var refreshToken = data.Data.Item2;

            _cookieService.SetTokenCookie(this.Response, refreshToken.ToString());

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

        [HttpPost]
        [Authorize]
        [Route("refresh-token")]
        public async Task<IActionResult> GetRefreshToken([FromBody] RefreshTokenRequest request)
        {
            var token = Request.Cookies[_configuration["TokenCookieName"]];

            if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
            {
                return BadRequest("Token cookie is required for this operation.");
            }

            request.Token = token;
            
            var data = await _mediator.Send(request);
            var response = data.Data.Item1;
            var refreshToken = data.Data.Item2;
            
            _cookieService.SetTokenCookie(this.Response, refreshToken.ToString());

            return Ok(response);
        }
    }
}