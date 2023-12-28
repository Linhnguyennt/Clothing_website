using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace week05_PTW.Models
{
    public class LogoutViewModel : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LogoutViewModel> _logger;
        public LogoutViewModel(SignInManager<IdentityUser> signInManager, ILogger<LogoutViewModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                // This needs to be a redirect so that the browser performs a new
                // request and the identity for the user gets updated.
                return RedirectToPageResult();
            }
        }

        private IActionResult RedirectToPageResult()
        {
            throw new NotImplementedException();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
