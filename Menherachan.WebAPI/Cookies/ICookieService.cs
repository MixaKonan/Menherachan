using Microsoft.AspNetCore.Http;

namespace Menherachan.WebAPI.Cookies
{
    public interface ICookieService
    {
        public void SetTokenCookie(HttpResponse response, string token);
        public void RemoveTokenCookie(HttpResponse response);
    }
}