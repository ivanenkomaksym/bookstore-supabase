using Supabase.Gotrue;

namespace WebUI.Services
{
    public interface IAuthService
    {
        public Task Login(string email, string password);

        public Task Logout();

        public Task<User?> GetUser();
    }
}
