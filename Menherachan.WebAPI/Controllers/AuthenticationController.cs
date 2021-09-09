using System;
using System.Threading.Tasks;
using MediatR;
using Menherachan.Application.CQRS.Commands.Authentication;
using Menherachan.WebAPI.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Menherachan.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v{version}/authentication/")]
    public class AuthenticationController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public AuthenticationController(
            IMediator mediator,
            IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest request)
        {
            var data = await _mediator.Send(request);
            var refreshToken = data.Data.Item2;

            SetTokenCookie(refreshToken.ToString());

            return Ok(data);
        }

        [HttpPost]
        [Authorize]
        [Route("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete(_configuration["Jwt:TokenCookieName"]);

            return Ok("Successfully logged out.");
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var token = Request.Cookies[_configuration["Jwt:TokenCookieName"]];

            if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
            {
                return Unauthorized("No required cookies found.");
            }

            var request = new RefreshTokenRequest(token);

            var data = await _mediator.Send(request);
            var refreshToken = data.Data.Item2;

            SetTokenCookie(refreshToken.ToString());

            return Ok(data);
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append(_configuration["Jwt:TokenCookieName"], token, cookieOptions);
        }
    }
}