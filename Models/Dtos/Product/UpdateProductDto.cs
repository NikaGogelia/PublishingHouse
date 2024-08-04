using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Models.Dtos.Product;

public class UpdateProductDto
{
	[Required]
	public bool IsArchived { get; set; }
}
