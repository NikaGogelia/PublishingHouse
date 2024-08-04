using PublishingHouse.Models.Dtos.Author;
using PublishingHouse.Models.Dtos.ProductType;
using PublishingHouse.Models.Dtos.Publisher;

namespace PublishingHouse.Models.Dtos.Product;

public class ProductByIdDto
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Annotation { get; set; }
	public ProductTypeDto ProductType { get; set; }
	public string ISBN { get; set; }
	public DateTime ReleaseDate { get; set; }
	public PublisherDto Publisher { get; set; }
	public int? NumberOfPages { get; set; }
	public string Address { get; set; }
	public bool IsArchived { get; set; }
	public ICollection<AuthorDto> Authors { get; set; }
}
