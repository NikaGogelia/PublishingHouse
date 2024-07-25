using PublishingHouse.Data;
using PublishingHouse.Models;
using PublishingHouse.Repository.IRepository;

namespace PublishingHouse.Repository
{
	public class CountryRepository : Repository<Country>, ICounrtyRepository
	{
		public CountryRepository(AppDbContext db) : base(db)
		{ }
	}
}
