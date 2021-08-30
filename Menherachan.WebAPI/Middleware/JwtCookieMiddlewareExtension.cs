using Microsoft.AspNetCore.Builder;

namespace Menherachan.WebAPI.Middleware
{
    public static class JwtCookieMiddlewareExtension
    {
        public static IApplicationBuilder UseJwtInCookies(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtCookieMiddleware>();
        }
    }
}