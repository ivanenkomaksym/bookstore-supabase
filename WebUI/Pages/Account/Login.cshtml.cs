using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUI.Services;

namespace WebUI.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IAuthService _authService;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(IAuthService authService, ILogger<LoginModel> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        
        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
                return Page();

            try
            {
                await _authService.Login(Input.Email, Input.Password);

                var user = await _authService.GetUser();

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }

                _logger.LogInformation("User {Email} logged in at {Time}.",
                    user.Email, DateTime.UtcNow);

                return Redirect("/Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(nameof(Input.Email), "Login Failed: Invalid Email or Password");
            }

            return Page();
        }
    }
}
