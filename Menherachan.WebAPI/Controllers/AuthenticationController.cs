using System;
using System.Threading.Tasks;
using MediatR;
using Menherachan.Application.CQRS.Commands.Authentication;
using Microsoft.AspNetCore.Http;
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
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest request)
        {
            var response = await _mediator.Send(request);
            
            SetTokenCookie(response.Data.Token.ToString());
            
            return Ok();
        }
        
        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("AspNetCore.Application.Id", token, cookieOptions);
        }
    }
}