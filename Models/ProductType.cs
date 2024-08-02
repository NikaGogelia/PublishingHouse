using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Models;

public class ProductType
{
	[Key]
	public int Id { get; set; }

	[Required]
	[RegularExpression("Book|Article|EBook", ErrorMessage = "Title must be either 'Article', 'Blog' or 'EBook'.")]
	public required string Type { get; set; }
}
