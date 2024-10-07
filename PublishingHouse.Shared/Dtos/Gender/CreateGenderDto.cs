using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Models.Dtos.Gender;

public class CreateGenderDto
{
	[Required]
	[RegularExpression("Male|Female", ErrorMessage = "Title must be either 'Male' or 'Female'.")]
	public required string Title { get; set; }
}
