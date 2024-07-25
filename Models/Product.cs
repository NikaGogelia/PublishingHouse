using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublishingHouse.Models;

public class Product
{
	[Key]
	public int Id { get; set; }

	[Required]
	[MinLength(2)]
	[MaxLength(250)]
	public required string Title { get; set; }

	[Required]
	[MinLength(100)]
	[MaxLength(500)]
	public required string Description { get; set; }

	[ForeignKey(nameof(ProductType))]
	public int TypeId { get; set; }
	public ProductType ProductType { get; set; }

	[Required]
	[RegularExpression(@"^\d{13}$", ErrorMessage = "ISBN must be exactly 13 digits.")]
	public required string ISBN { get; set; }

	[Required]
	[DataType(DataType.Date)]
	public required DateTime ReleaseDate { get; set; }

	[ForeignKey(nameof(Publisher))]
	public int PublisherId { get; set; }
	public Publisher Publisher { get; set; }
}
