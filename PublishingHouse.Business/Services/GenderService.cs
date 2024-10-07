using AutoMapper;
using PublishingHouse.Business.Services.Interfaces;
using PublishingHouse.Models;
using PublishingHouse.Models.Dtos.Gender;
using PublishingHouse.Repository.IRepository;

namespace PublishingHouse.Business.Services;

public class GenderService : IGenderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GenderService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GenderDto>> GetAllGenders()
    {
        var genders = await _unitOfWork.Gender.GetAllAsync();

        var result = _mapper.Map<IEnumerable<GenderDto>>(genders);

        return result;
    }

    public async Task<GenderDto> GetGender(int id)
    {
        var gender = await _unitOfWork.Gender.GetByIdAsync(gender => gender.Id == id);

        if (gender == null)
        {
            throw new KeyNotFoundException($"Gender with id {id} not found.");
        }

        var result = _mapper.Map<GenderDto>(gender);

        return result;
    }

    public async Task<GenderDto> CreateGender(CreateGenderDto entity)
    {
        var gender = _mapper.Map<Gender>(entity);

        var create = await _unitOfWork.Gender.AddAsync(gender);
        await _unitOfWork.Save();

        var result = _mapper.Map<GenderDto>(create);

        return result;
    }
}
