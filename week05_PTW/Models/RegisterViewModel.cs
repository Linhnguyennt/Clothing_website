using System.ComponentModel.DataAnnotations;
namespace week05_PTW.Models
	
{
	public class RegisterViewModel
	{
			[Required]
			[EmailAddress]
			[Display(Name = "Email")]
			public string Email { get; set; }

			/// <summary>
			///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
			///     directly from your code. This API may change or be removed in future releases.
			/// </summary>
			[Required]
			[StringLength(10, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
			[DataType(DataType.Password)]
			[Display(Name = "Password")]
			public string Password { get; set; }

			/// <summary>
			///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
			///     directly from your code. This API may change or be removed in future releases.
			/// </summary>
			[DataType(DataType.Password)]
			[Display(Name = "Confirm password")]
			[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
			public string ConfirmPassword { get; set; }

			[StringLength(50, ErrorMessage = "Only up to 50 characters are allowed")]
			[Display(Name = "First Name")]
			[RegularExpression("^([a-zA-Z0-9 .&'-]+)$", ErrorMessage = "Invalid First Name")]
			public string FirstName { get; set; }

			[Display(Name = "Last Name")]
			[RegularExpression("^([a-zA-Z0-9 .&'-]+)$", ErrorMessage = "Invalid First Name")]
			[StringLength(50, ErrorMessage = "Only up to 50 characters are allowed")]
			public string LastName { get; set; }
			[RegularExpression("^([a-zA-Z0-9 ,.&'-]+)$", ErrorMessage = "Invalid characters")]
			public string? Address { get; set; }

			[StringLength(10, ErrorMessage = "Only up to 10 characters are allowed", MinimumLength = 10)]
			[Display(Name = "Phone Number")]

			//[RegularExpression(pattern: "^Mr\\..*|^Mrs\\..*|^Ms\\..*|^Miss\\..*", ErrorMessage = "Name must start with Mr./Mrs./Ms./Miss.")]
			[RegularExpression("^([0-9]+)$", ErrorMessage = "Invalid Phone Number")]
			public string? PhoneNumber { get; set; }
		}
	}

