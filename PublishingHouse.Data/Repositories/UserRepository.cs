using Microsoft.EntityFrameworkCore;
using PublishingHouse.Data.Entities;
using PublishingHouse.Data.Repositories.Interfaces;

namespace PublishingHouse.Data.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly AppDbContext _db;

		public UserRepository(AppDbContext db)
		{
			_db = db;
		}

		public async Task AddUserAsync(User user)
		{
			await _db.Users.AddAsync(user);
		}

		public async Task<User> GetUserByUserNameAsync(string userName)
		{
			var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == userName);

			return user;
		}
	}
}
