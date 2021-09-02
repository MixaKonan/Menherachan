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
        private readonly IAdminService _adminService;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IMediator mediator,
            ICookieService cookieService,
            IConfiguration configuration,
            IAdminService adminService)
        {
            _mediator = mediator;
            _cookieService = cookieService;
            _adminService = adminService;
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
        [Route("/refresh-token")]
        public async Task<IActionResult> GetRefreshToken()
        {
            var refreshToken = Request.Cookies[_configuration["TokenCookieName"]];

            if (string.IsNullOrEmpty(refreshToken) || string.IsNullOrWhiteSpace(refreshToken))
            {
                return BadRequest("Token cookie is required for this operation");
            }

            var newToken = await _adminService.RefreshAdminToken();

            _cookieService.SetTokenCookie(this.Response, newToken.ToString());

            return Ok("Token refreshed");
        }
    }
}