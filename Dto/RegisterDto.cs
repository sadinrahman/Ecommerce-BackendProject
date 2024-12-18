using System.ComponentModel.DataAnnotations;

namespace BackendProject.Dto
{
	public class RegisterDto
	{
		[Required(ErrorMessage = "Name is required")]
		public string? UserName { get; set; }
		[Required]
		[EmailAddress(ErrorMessage = "Enter proper Email")]
		public string? Email { get; set; }
		[Required(ErrorMessage = "Password is required")]
		[MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
		[RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
		ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
		public string? Password { get; set; }
		[Required]
		[Compare("Password", ErrorMessage = "password and confirmation password do not match.")]
		public string? ConformPassword { get; set; }
	}
}
