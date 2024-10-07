using System.ComponentModel.DataAnnotations;
using PublishingHouse.Shared.Enums;

namespace PublishingHouse.Data.Entities;

public class User
{
	public int Id { get; set; }

	[Required(ErrorMessage = "Username is required.")]
	[StringLength(20, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 20 characters.")]
	public string Username { get; set; }

	[Required(ErrorMessage = "Email is required.")]
	[EmailAddress(ErrorMessage = "Invalid email format.")]
	public string Email { get; set; }

	[Required(ErrorMessage = "Password is required.")]
	[StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
	[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{6,}$",
		ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
	public string Password { get; set; }

	[Required(ErrorMessage = "Role is required.")]
	public UserRole Role { get; set; }
}
