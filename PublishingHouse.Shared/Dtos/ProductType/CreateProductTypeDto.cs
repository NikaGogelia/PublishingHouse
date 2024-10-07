using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Models.Dtos.ProductType;

public class CreateProductTypeDto
{
	[Required]
	[RegularExpression("Book|Article|EBook", ErrorMessage = "Title must be either 'Book', 'Article' or 'EBook'.")]
	public required string Type { get; set; }
}
