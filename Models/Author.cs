using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublishingHouse.Models;

public class Author
{
	[Key]
	public int Id { get; set; }

	[Required]
	[MinLength(2)]
	[MaxLength(50)]
	public required string FirstName { get; set; }

	[Required]
	[MinLength(2)]
	[MaxLength(50)]
	public required string LastName { get; set; }

	[ForeignKey(nameof(Gender))]
	public int GenderId { get; set; }
	public Gender Gender { get; set; }

	[Required]
	[RegularExpression(@"^\d{11}$", ErrorMessage = "Personal number must be exactly 11 digits.")]
	public required string PersonalNumber { get; set; }

	[Required]
	[DataType(DataType.Date)]
	[CustomValidation(typeof(Author), nameof(ValidateAge))]
	public required DateTime DateOfBirth { get; set; }

	[ForeignKey(nameof(Country))]
	public int CountryId { get; set; }
	public Country Country { get; set; }

	[ForeignKey(nameof(City))]
	public int CityId { get; set; }
	public City City { get; set; }

	[Phone]
	[MinLength(4)]
	[MaxLength(50)]
	public string PhoneNumber { get; set; }

	[Required]
	[EmailAddress]
	public string Email { get; set; }

	public ICollection<ProductAuthor> ProductAuthors { get; set; }

	public static ValidationResult ValidateAge(DateTime dateOfBirth, ValidationContext context)
	{
		var age = DateTime.Today.Year - dateOfBirth.Year;
		if (dateOfBirth > DateTime.Today.AddYears(-age)) age--;

		return age >= 18
			? ValidationResult.Success
			: new ValidationResult("Author must be at least 18 years old.");
	}
}
