using PublishingHouse.Models.Dtos.City;

namespace PublishingHouse.Business.Services.Interfaces;

public interface ICityService
{
    Task<IEnumerable<CityDto>> GetAllCities();
    Task<CityDto> GetCity(int id);
    Task<CityDto> CreateCity(CreateCityDto entity);
    Task<CityDto> EditCity(int id, UpdateCityDto entity);
    Task<CityDto> RemoveCity(int id);
}
