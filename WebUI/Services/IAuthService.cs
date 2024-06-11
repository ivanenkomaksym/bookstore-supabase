using Supabase.Gotrue;

namespace WebUI.Services
{
    public interface IAuthService
    {
        public Task<Session?> Login(string email, string password);

        public Task<User?> TryLoginWithCookies(HttpContext httpContext);

        public Task Logout();

        public Task<User?> GetUser();
    }
}
