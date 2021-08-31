using System.Net;
using Menherachan.Domain.Entities.Responses;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Menherachan.WebAPI.Controllers
{
    public class ErrorController : Controller
    {
        // GET
        public IActionResult Error()
        {
            var response = new Response<dynamic>();
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;
            response.Data.StatusCode = 500;

            if (exception is WebException)
            {
                response.Data.StatusCode = 404;
            }

            else
            {
                response.Message = exception.Message;
            }


            return Ok(response);
        }
    }
}