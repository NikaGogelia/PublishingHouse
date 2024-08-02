using PublishingHouse.Models.Dtos.Gender;

namespace PublishingHouse.Services.IServices;

public interface IGenderService
{
	Task<IEnumerable<GenderDto>> GetAllGenders();
	Task<GenderDto> GetGender(int id);
	Task<GenderDto> CreateGender(CreateGenderDto entity);
}
