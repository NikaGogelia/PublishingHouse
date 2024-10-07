using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Models.Dtos.City;

public class UpdateCityDto
{
	[Required]
	public required string Title { get; set; }
}
