using Menherachan.Application.CQRS.Commands.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Menherachan.WebAPI.Controllers.v1
{
    [ApiController]
    [Route("api/v{version}/authentication/")]
    public class AuthenticationController : Controller
    {
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticationRequest request)
        {
            return Ok();
        }
    }
}