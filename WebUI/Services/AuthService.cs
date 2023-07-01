using Microsoft.AspNetCore.Components.Authorization;
using Supabase.Gotrue;

namespace WebUI.Services
{
    public class AuthService : IAuthService
    {
        private readonly Supabase.Client _client;
        private readonly AuthenticationStateProvider _customAuthStateProvider;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            Supabase.Client client,
            AuthenticationStateProvider customAuthStateProvider,
            ILogger<AuthService> logger
        )
        {
            logger.LogInformation("------------------- CONSTRUCTOR -------------------");

            _client = client;
            _customAuthStateProvider = customAuthStateProvider;
            _logger = logger;
        }

        public async Task Login(string email, string password)
        {
            _logger.LogInformation("METHOD: Login");

            var session = await _client.Auth.SignIn(email, password);

            _logger.LogInformation("------------------- User logged in -------------------");
            // logger.LogInformation($"instance.Auth.CurrentUser.Id {client?.Auth?.CurrentUser?.Id}");
            _logger.LogInformation($"client.Auth.CurrentUser.Email {_client?.Auth?.CurrentUser?.Email}");

            await _customAuthStateProvider.GetAuthenticationStateAsync();
        }

        public async Task Logout()
        {
            await _client.Auth.SignOut();
            await _customAuthStateProvider.GetAuthenticationStateAsync();
        }

        public async Task<User?> GetUser()
        {
            var session = await _client.Auth.RetrieveSessionAsync();
            return session?.User;
        }

    }
}
