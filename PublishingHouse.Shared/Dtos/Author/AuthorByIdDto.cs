using PublishingHouse.Models.Dtos.City;
using PublishingHouse.Models.Dtos.Country;
using PublishingHouse.Models.Dtos.Gender;
using PublishingHouse.Models.Dtos.Product;

namespace PublishingHouse.Models.Dtos.Author;

public class AuthorByIdDto
{
	public int Id { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public GenderDto Gender { get; set; }
	public string PersonalNumber { get; set; }
	public DateTime DateOfBirth { get; set; }
	public CountryDto Country { get; set; }
	public CityDto City { get; set; }
	public string PhoneNumber { get; set; }
	public string Email { get; set; }
	public ICollection<ProductDto> Products { get; set; }
}
