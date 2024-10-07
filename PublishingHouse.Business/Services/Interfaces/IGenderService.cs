using PublishingHouse.Models.Dtos.Gender;

namespace PublishingHouse.Business.Services.Interfaces;

public interface IGenderService
{
    Task<IEnumerable<GenderDto>> GetAllGenders();
    Task<GenderDto> GetGender(int id);
    Task<GenderDto> CreateGender(CreateGenderDto entity);
}
