using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Models;

public class City
{
	[Key]
	public int Id { get; set; }

	[Required]
	public required string Title { get; set; }
}
