using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Models.Dtos.ProductAuthor;

public class UpdateProductAuthorDto
{
	[Required]
	public int ProductId { get; set; }

	[Required]
	public int AuthorId { get; set; }
}
