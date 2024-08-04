using System.ComponentModel.DataAnnotations;

namespace PublishingHouse.Models.Dtos.Product;

public class CreateProductDto
{
	[Required]
	[MinLength(2)]
	[MaxLength(250)]
	public string Name { get; set; }

	[Required]
	[MinLength(100)]
	[MaxLength(500)]
	public string Annotation { get; set; }

	[Required]
	public int ProductTypeId { get; set; }

	[Required]
	[RegularExpression(@"^\d{13}$", ErrorMessage = "ISBN must be exactly 13 digits.")]
	public string ISBN { get; set; }

	[Required]
	[DataType(DataType.Date)]
	public DateTime ReleaseDate { get; set; }

	[Required]
	public int PublisherId { get; set; }

	public int? NumberOfPages { get; set; }

	[Required]
	[MaxLength(1000)]
	public string Address { get; set; }

	[Required]
	public bool IsArchived { get; set; }
}
