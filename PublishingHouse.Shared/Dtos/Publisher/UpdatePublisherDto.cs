using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Models.Dtos.Publisher;

public class UpdatePublisherDto
{
	[Required]
	public string Title { get; set; }
}
