using PublishingHouse.Data;
using PublishingHouse.Models;
using PublishingHouse.Repository.IRepository;

namespace PublishingHouse.Repository;

public class CityRepository : Repository<City>, ICityRepository
{
	public CityRepository(AppDbContext db) : base(db)
	{ }
}
