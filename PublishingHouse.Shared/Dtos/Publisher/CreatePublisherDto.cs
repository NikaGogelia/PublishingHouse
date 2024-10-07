using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Models.Dtos.Publisher;

public class CreatePublisherDto
{
	[Required]
	public string Title { get; set; }
}
