using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Models.Dtos.Author;

public class UpdateAuthorDto
{
	[Required]
	[MinLength(2)]
	[MaxLength(50)]
	public string FirstName { get; set; }

	[Required]
	[MinLength(2)]
	[MaxLength(50)]
	public string LastName { get; set; }

	[Required]
	public int GenderId { get; set; }

	[Required]
	[RegularExpression(@"^\d{11}$", ErrorMessage = "Personal number must be exactly 11 digits.")]
	public string PersonalNumber { get; set; }

	[Required]
	[DataType(DataType.Date)]
	[CustomValidation(typeof(UpdateAuthorDto), nameof(ValidateAge))]
	public DateTime DateOfBirth { get; set; }

	[Required]
	public int CountryId { get; set; }

	[Required]
	public int CityId { get; set; }

	[MinLength(4)]
	[MaxLength(50)]
	public string PhoneNumber { get; set; }

	[Required]
	[EmailAddress]
	public string Email { get; set; }

	public static ValidationResult ValidateAge(DateTime dateOfBirth, ValidationContext context)
	{
		var age = DateTime.Today.Year - dateOfBirth.Year;
		if (dateOfBirth > DateTime.Today.AddYears(-age)) age--;

		return age >= 18
			? ValidationResult.Success
			: new ValidationResult("Author must be at least 18 years old.");
	}
}
