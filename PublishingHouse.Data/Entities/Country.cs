using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Models;

public class Country
{
	[Key]
	public int Id { get; set; }

	[Required]
	public required string Title { get; set; }
}
