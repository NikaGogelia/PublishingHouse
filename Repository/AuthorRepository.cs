using PublishingHouse.Data;
using PublishingHouse.Models;
using PublishingHouse.Repository.IRepository;

namespace PublishingHouse.Repository
{
	public class AuthorRepository : Repository<Author>, IAuthorRepository
	{
		public AuthorRepository(AppDbContext db) : base(db)
		{ }
	}
}
