namespace WebUI.Middlewares
{
    public static class CookieAuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseCookieAuthentication(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CookieAuthenticationMiddleware>();
        }
    }
}
