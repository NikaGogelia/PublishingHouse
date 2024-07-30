namespace PublishingHouse.Models.Dtos.ProductAuthor;

public class ProductAuthorDto
{
	public int Id { get; set; }
	public int ProductId { get; set; }
	public string ProductName { get; set; }
	public int AuthorId { get; set; }
	public string AuthorFullName { get; set; }
}
