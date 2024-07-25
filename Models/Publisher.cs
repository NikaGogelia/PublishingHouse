using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Models;

public class Publisher
{
	[Key]
	public int Id { get; set; }

	[Required]
	public string Title { get; set; }
}
