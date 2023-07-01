using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUI.Services;

namespace WebUI.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly IAuthService _authService;
        private readonly ILogger<LoginModel> _logger;

        public LogoutModel(IAuthService authService, ILogger<LoginModel> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            _logger.LogInformation("User {Name} logged out at {Time}.", User.Identity.Name, DateTime.UtcNow);

            await _authService.Logout();

            return RedirectToPage("SignedOut");
        }
    }
}
