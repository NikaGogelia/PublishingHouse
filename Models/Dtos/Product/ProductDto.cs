using PublishingHouse.Models.Dtos.Author;

namespace PublishingHouse.Models.Dtos.Product;

public class ProductDto
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Annotation { get; set; }
	public int ProductTypeId { get; set; }
	public string ProductTypeTitle { get; set; }
	public string ISBN { get; set; }
	public DateTime ReleaseDate { get; set; }
	public int PublisherId { get; set; }
	public string PublisherTitle { get; set; }
	public int? NumberOfPages { get; set; }
	public string Address { get; set; }
	public ICollection<AuthorDto> Authors { get; set; }
}
