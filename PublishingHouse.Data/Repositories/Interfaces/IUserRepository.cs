using PublishingHouse.Data.Entities;

namespace PublishingHouse.Data.Repositories.Interfaces;

public interface IUserRepository
{
	Task AddUserAsync(User user);
	Task<User> GetUserByUserNameAsync(string userName);
}
