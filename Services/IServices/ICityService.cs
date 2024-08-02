using PublishingHouse.Models.Dtos.City;

namespace PublishingHouse.Services.IServices;

public interface ICityService
{
	Task<IEnumerable<CityDto>> GetAllCities();
	Task<CityDto> GetCity(int id);
	Task<CityDto> CreateCity(CreateCityDto entity);
	Task<CityDto> EditCity(int id, UpdateCityDto entity);
	Task<CityDto> RemoveCity(int id);
}
