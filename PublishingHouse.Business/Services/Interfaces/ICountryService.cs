using PublishingHouse.Models.Dtos.Country;

namespace PublishingHouse.Business.Services.Interfaces;

public interface ICountryService
{
    Task<IEnumerable<CountryDto>> GetAllCountries();
    Task<CountryDto> GetCountry(int id);
    Task<CountryDto> CreateCountry(CreateCountryDto entity);
    Task<CountryDto> EditCountry(int id, UpdateCountryDto entity);
    Task<CountryDto> RemoveCountry(int id);
}
