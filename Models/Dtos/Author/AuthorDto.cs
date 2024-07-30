using PublishingHouse.Models.Dtos.Product;

namespace PublishingHouse.Models.Dtos.Author;

public class AuthorDto
{
	public int Id { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public int GenderId { get; set; }
	public string GenderName { get; set; }
	public string PersonalNumber { get; set; }
	public DateTime DateOfBirth { get; set; }
	public int CountryId { get; set; }
	public string CountryName { get; set; }
	public int CityId { get; set; }
	public string CityName { get; set; }
	public string PhoneNumber { get; set; }
	public string Email { get; set; }
	public ICollection<ProductDto> Products { get; set; }
}
