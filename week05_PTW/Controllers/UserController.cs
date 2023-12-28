using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using week05_PTW.Data;
using week05_PTW.Models;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;

namespace week05_PTW.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IUserStore<AppUser> _userStore;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly WebDbContext _context;

		private readonly ILogger<LoginModel> _logger;

		public UserController(UserManager<AppUser> userManager,
			IUserStore<AppUser> userStore,
			SignInManager<AppUser> signInManager,
			WebDbContext context)
		{

			_context = context;
			_userManager = userManager;
			_userStore = userStore;

			_signInManager = signInManager;


		}
		[HttpPost]
		public async Task<IActionResult> Register(Models.RegisterViewModel model)
		{
			var returnUrl = Url.Content("~/");
			//ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
			if (ModelState.IsValid)
			{
				var user = CreateUser();
				user.FirstName = model.FirstName;
				user.LastName = model.LastName;
				user.Address = model.Address;
				user.PhoneNumber = model.PhoneNumber;

				await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
				//await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
				var result = await _userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{
					// _logger.LogInformation("User created a new account with password.");

					var userId = await _userManager.GetUserIdAsync(user);
					var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
					code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
					var callbackUrl = Url.Page(
						"/Account/ConfirmEmail",
						pageHandler: null,
						values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
						protocol: Request.Scheme);

					//await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
					// $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

					if (_userManager.Options.SignIn.RequireConfirmedAccount)
					{
						return RedirectToPage("RegisterConfirmation", new { email = model.Email, returnUrl = returnUrl });
					}
					else
					{
						await _signInManager.SignInAsync(user, isPersistent: false);
						return RedirectToAction("Index", "Home");
					}
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}

			// If we got this far, something failed, redisplay form

			return View();
		}
		private AppUser CreateUser()
		{
			try
			{
				return Activator.CreateInstance<AppUser>();
			}
			catch
			{
				throw new InvalidOperationException($"Can't create an instance of '{nameof(AppUser)}'. " +
					$"Ensure that '{nameof(AppUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
					$"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
			}
		}




		public IActionResult Register()
		{
			return View();
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(Models.LoginViewModel model, string? returnUrl = "~/")
		{
			// var returnUrl= Url.Content("~/");

			// ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

			if (ModelState.IsValid)
			{
				// This doesn't count login failures towards account lockout
				// To enable password failures to trigger account lockout, set lockoutOnFailure: true
				var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
				if (result.Succeeded)
				{
					//_logger.LogInformation("User logged in.");
					//return RedirectToAction("Index", "Home");
					return Redirect(returnUrl);
					//return RedirectToPage("/Admin/Create");

				}
				//if (result.RequiresTwoFactor)
				//{
				//    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
				//}
				if (result.IsLockedOut)
				{
					_logger.LogWarning("User account locked out.");
					//return RedirectToPage("./Lockout");
					return RedirectToPage("Login");
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Invalid login attempt.");
					return RedirectToPage("./Lockout");
				}
			}

			// If we got this far, something failed, redisplay form

			return View();
		}
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			//_logger.LogInformation("User logged out.");
			String returnUrl = "~/";


			return LocalRedirect(returnUrl);

		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
