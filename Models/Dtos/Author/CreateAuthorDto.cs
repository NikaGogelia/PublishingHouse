using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Models.Dtos.Author;

public class CreateAuthorDto
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
}
