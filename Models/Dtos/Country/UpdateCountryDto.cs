using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Models.Dtos.Country;

public class UpdateCountryDto
{
	[Required]
	public required string Title { get; set; }
}
