using PublishingHouse.Models.Dtos.Author;
using PublishingHouse.Models.Dtos.Product;
using System.Text.Json.Serialization;

namespace PublishingHouse.Models.Dtos.ProductAuthor;

public class ProductAuthorDto
{
	public int Id { get; set; }
	public ProductDto Product { get; set; }
	public AuthorDto Author { get; set; }
}

