using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublishingHouse.Models;

public class Product : IValidatableObject
{
	[Key]
	public int Id { get; set; }

	[Required]
	[MinLength(2)]
	[MaxLength(250)]
	public string Name { get; set; }

	[Required]
	[MinLength(100)]
	[MaxLength(500)]
	public string Annotation { get; set; }

	[Required]
	[ForeignKey(nameof(ProductType))]
	public int ProductTypeId { get; set; }
	public ProductType ProductType { get; set; }

	[Required]
	[RegularExpression(@"^\d{13}$", ErrorMessage = "ISBN must be exactly 13 digits.")]
	public string ISBN { get; set; }

	[Required]
	[DataType(DataType.Date)]
	public DateTime ReleaseDate { get; set; }

	[Required]
	public int PublisherId { get; set; }

	public Publisher Publisher { get; set; }

	public int? NumberOfPages { get; set; }

	[Required]
	[MaxLength(1000)]
	public string Address { get; set; }

	[Required]
	public bool IsArchived { get; set; }

	public ICollection<ProductAuthor> ProductAuthors { get; set; }

	public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
	{
		if (ProductType == null)
		{
			yield return new ValidationResult("ProductType cannot be null.");
		}
		else if (ProductType.Type == "Book" && string.IsNullOrWhiteSpace(Address))
		{
			yield return new ValidationResult("Physical address is required for books.", new[] { nameof(Address) });
		}
		else if ((ProductType.Type == "Article" || ProductType.Type == "EBook") && !Uri.IsWellFormedUriString(Address, UriKind.Absolute))
		{
			yield return new ValidationResult("A valid URL is required for articles and ebooks.", new[] { nameof(Address) });
		}
	}
}
