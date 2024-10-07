using PublishingHouse.Shared.Dtos.Auth;

namespace PublishingHouse.Business.Services.Interfaces;

public interface IUserService
{
	Task<string> RegisterAsync(RegisterDto model);
	Task<AuthResponseDto> LoginAsync(LoginDto model);
}
