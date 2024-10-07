using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Models.Dtos.City;

public class CreateCityDto
{
	[Required]
	public required string Title { get; set; }
}
