using AutoMapper;
using PublishingHouse.Business.Services.Interfaces;
using PublishingHouse.Models;
using PublishingHouse.Models.Dtos.City;
using PublishingHouse.Repository.IRepository;

namespace PublishingHouse.Business.Services;

public class CityService : ICityService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CityService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CityDto>> GetAllCities()
    {
        var cities = await _unitOfWork.City.GetAllAsync();

        var result = _mapper.Map<IEnumerable<CityDto>>(cities);

        return result;
    }

    public async Task<CityDto> GetCity(int id)
    {
        var city = await _unitOfWork.City.GetByIdAsync(city => city.Id == id);

        if (city == null)
        {
            throw new KeyNotFoundException($"City with id {id} not found.");
        }

        var result = _mapper.Map<CityDto>(city);

        return result;
    }

    public async Task<CityDto> CreateCity(CreateCityDto entity)
    {
        var city = _mapper.Map<City>(entity);

        var create = await _unitOfWork.City.AddAsync(city);
        await _unitOfWork.Save();

        var result = _mapper.Map<CityDto>(create);

        return result;
    }

    public async Task<CityDto> EditCity(int id, UpdateCityDto entity)
    {
        var check = await _unitOfWork.City.GetByIdAsync(city => city.Id == id);

        if (check == null)
        {
            throw new KeyNotFoundException($"City with id {id} not found.");
        }

        var city = _mapper.Map<City>(entity);

        city.Id = id;

        var update = await _unitOfWork.City.UpdateAsync(city);
        await _unitOfWork.Save();

        var result = _mapper.Map<CityDto>(update);

        return result;
    }

    public async Task<CityDto> RemoveCity(int id)
    {
        var check = await _unitOfWork.City.GetByIdAsync(city => city.Id == id);

        if (check == null)
        {
            throw new KeyNotFoundException($"City with id {id} not found.");
        }

        var delete = await _unitOfWork.City.DeleteAsync(id);
        await _unitOfWork.Save();

        var result = _mapper.Map<CityDto>(check);

        return result;
    }
}
