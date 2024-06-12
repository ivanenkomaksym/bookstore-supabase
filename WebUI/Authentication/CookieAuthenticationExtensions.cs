using Supabase.Gotrue;
using WebUI.Services;

namespace WebUI.Authentication
{
    public static class CookieAuthenticationExtensions
    {
        public static async Task<User?> TryLoginWithCookies(this HttpContext httpContext, IAuthService authService)
        {
            var hasSessionId = httpContext.TryGetRefreshToken(out var refreshToken);
            if (hasSessionId)
            {
                var session = await authService.TryLoginWithRefreshToken(refreshToken);

                httpContext.AddOrUpdateRefreshToken(session.RefreshToken);

                return session.User;
            }

            return null;
        }

        public static async Task Logout(this HttpContext httpContext, IAuthService authService)
        {
            await authService.Logout();
            httpContext.Response.Cookies.Delete(SessionId);
        }

        private static void AddOrUpdateRefreshToken(this HttpContext httpContext, string refreshToken)
        {
            httpContext.Response.Cookies.Append(SessionId, refreshToken);
        }

        private static bool TryGetRefreshToken(this HttpContext httpContext, out string refreshToken)
        {
            return httpContext.Request.Cookies.TryGetValue(SessionId, out refreshToken);
        }

        private static readonly string SessionId = "sessionId";
    }
}
