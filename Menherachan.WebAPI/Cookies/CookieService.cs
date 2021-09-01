using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Menherachan.WebAPI.Cookies
{
    public class CookieService : ICookieService
    {
        private string CookieName { get; set; }
        
        public CookieService(IConfiguration configuration)
        {
            CookieName = configuration["Jwt:TokenCookieName"];
        }

        public void SetTokenCookie(HttpResponse response, string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            
            response.Cookies.Append(CookieName, token, cookieOptions);
        }
        
        public void RemoveTokenCookie(HttpResponse response)
        {
            response.Cookies.Delete(CookieName);
        }
    }
}