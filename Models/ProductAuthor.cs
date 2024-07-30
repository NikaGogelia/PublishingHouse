using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Models;

public class ProductAuthor
{
	[Key]
	public int Id { get; set; }

	[Required]
	public int ProductId { get; set; }
	public Product Product { get; set; }

	[Required]
	public int AuthorId { get; set; }
	public Author Author { get; set; }
}
