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

        public async Task<Session?> Login(string email, string password)
        {
            _logger.LogInformation("METHOD: Login");

            var session = await _client.Auth.SignIn(email, password);

            _logger.LogInformation("------------------- User logged in -------------------");
            // logger.LogInformation($"instance.Auth.CurrentUser.Id {client?.Auth?.CurrentUser?.Id}");
            _logger.LogInformation($"client.Auth.CurrentUser.Email {_client?.Auth?.CurrentUser?.Email}");

            await _customAuthStateProvider.GetAuthenticationStateAsync();

            return session;
        }

        public async Task<User?> TryLoginWithCookies(HttpContext httpContext)
        {
            var hasSessionId = httpContext.Request.Cookies.TryGetValue("sessionId", out var refreshToken);
            if (hasSessionId)
            {
                try
                {
                    var session = await TryLoginWithRefreshToken(refreshToken);

                    httpContext.Response.Cookies.Append("sessionId", session.RefreshToken);

                    return session.User;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to login with refresh token from cookies.");
                }
            }

            return null;
        }

        private async Task<Session?> TryLoginWithRefreshToken(string accessToken)
        {
            _logger.LogInformation("METHOD: TryLogin");

            var session = await _client.Auth.SignIn(Constants.SignInType.RefreshToken, accessToken);

            _logger.LogInformation("------------------- User logged in -------------------");
            // logger.LogInformation($"instance.Auth.CurrentUser.Id {client?.Auth?.CurrentUser?.Id}");
            _logger.LogInformation($"client.Auth.CurrentUser.Email {_client?.Auth?.CurrentUser?.Email}");

            return session;
        }

        public async Task Logout()
        {
            await _client.Auth.SignOut();
            await _customAuthStateProvider.GetAuthenticationStateAsync();
        }

        public async Task<User?> GetUser()
        {
            return _client.Auth.CurrentUser;
        }

    }
}
