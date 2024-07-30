using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Models.Dtos.ProductAuthor;

public class CreateProductAuthorDto
{
	[Required]
	public int ProductId { get; set; }

	[Required]
	public int AuthorId { get; set; }
}
