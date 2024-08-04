using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Identity.Models;
public class UserRole
{
	public string UserName { get; set; } = string.Empty;

	[ValidRole]
	public string Role { get; set; } = string.Empty;
}

public class ValidRoleAttribute : ValidationAttribute
{
	private readonly List<string> _allowedRoles = new List<string> { "Operator", "Senior Operator", "Manager" };

	protected override ValidationResult IsValid(object value, ValidationContext validationContext)
	{
		if (value is string role && _allowedRoles.Contains(role))
		{
			return ValidationResult.Success;
		}

		return new ValidationResult($"The role '{value}' is not valid. Allowed roles are: {string.Join(", ", _allowedRoles)}.");
	}
}