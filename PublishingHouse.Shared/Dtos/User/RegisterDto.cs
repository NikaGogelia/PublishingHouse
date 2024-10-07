﻿using PublishingHouse.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Shared.Dtos.Auth;

public class RegisterDto
{
	[Required(ErrorMessage = "Username is required.")]
	[StringLength(20, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 20 characters.")]
	public string Username { get; set; }

	[Required(ErrorMessage = "Email is required.")]
	[EmailAddress(ErrorMessage = "Invalid email format.")]
	public string Email { get; set; }

	[Required(ErrorMessage = "Password is required.")]
	[StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
	public string Password { get; set; }

	[Required(ErrorMessage = "Role is required.")]
	[EnumDataType(typeof(UserRole))]
	public UserRole Role { get; set; }
}
