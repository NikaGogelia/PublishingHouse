using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Models.Dtos.Country;

public class CreateCountryDto
{
	[Required]
	public required string Title { get; set; }
}
