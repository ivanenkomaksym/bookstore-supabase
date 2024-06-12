using Supabase.Gotrue;

namespace WebUI.Services
{
    public interface IAuthService
    {
        public Task<Session?> Login(string email, string password);

        public Task<Session?> TryLoginWithRefreshToken(string refreshToken);

        public Task Logout();

        public Task<User?> GetUser();
    }
}
