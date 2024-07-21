using WebUI.Authentication;
using WebUI.Services;

namespace WebUI.Middlewares
{
    /// <summary>
    /// This middleware make sure to always initialize user from cookies if possible despite location where interaction starts.
    /// </summary>
    public class CookieAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public CookieAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IAuthService authService)
        {
            var user = await context.GetUserOrTryLoginWithCookies(authService);

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
