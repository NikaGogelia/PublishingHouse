namespace PublishingHouse.Models.Dtos.Product;

public class ProductDto
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Annotation { get; set; }
	public string ISBN { get; set; }
	public DateTime ReleaseDate { get; set; }
	public int? NumberOfPages { get; set; }
	public string Address { get; set; }
	public bool IsArchived { get; set; }
}
