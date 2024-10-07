using AutoMapper;
using PublishingHouse.Business.Services.Interfaces;
using PublishingHouse.Models;
using PublishingHouse.Models.Dtos.Country;
using PublishingHouse.Repository.IRepository;

namespace PublishingHouse.Business.Services;

public class CountryService : ICountryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CountryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CountryDto>> GetAllCountries()
    {
        var countries = await _unitOfWork.Country.GetAllAsync();

        var result = _mapper.Map<IEnumerable<CountryDto>>(countries);

        return result;
    }

    public async Task<CountryDto> GetCountry(int id)
    {
        var country = await _unitOfWork.Country.GetByIdAsync(country => country.Id == id);

        if (country == null)
        {
            throw new KeyNotFoundException($"Country with id {id} not found.");
        }

        var result = _mapper.Map<CountryDto>(country);

        return result;
    }

    public async Task<CountryDto> CreateCountry(CreateCountryDto entity)
    {
        var country = _mapper.Map<Country>(entity);

        var create = await _unitOfWork.Country.AddAsync(country);
        await _unitOfWork.Save();

        var result = _mapper.Map<CountryDto>(create);

        return result;
    }

    public async Task<CountryDto> EditCountry(int id, UpdateCountryDto entity)
    {
        var check = await _unitOfWork.Country.GetByIdAsync(country => country.Id == id);

        if (check == null)
        {
            throw new KeyNotFoundException($"Country with id {id} not found.");
        }

        var country = _mapper.Map<Country>(entity);

        country.Id = id;

        var update = await _unitOfWork.Country.UpdateAsync(country);
        await _unitOfWork.Save();

        var result = _mapper.Map<CountryDto>(update);

        return result;
    }

    public async Task<CountryDto> RemoveCountry(int id)
    {
        var check = await _unitOfWork.Country.GetByIdAsync(country => country.Id == id);

        if (check == null)
        {
            throw new KeyNotFoundException($"Country with id {id} not found.");
        }

        var delete = await _unitOfWork.Country.DeleteAsync(id);
        await _unitOfWork.Save();

        var result = _mapper.Map<CountryDto>(check);

        return result;
    }
}
