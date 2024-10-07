using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Models;

public class Gender
{
	[Key]
	public int Id { get; set; }

	[Required]
	[RegularExpression("Male|Female", ErrorMessage = "Title must be either 'Male' or 'Female'.")]
	public required string Title { get; set; }
}
