using PublishingHouse.Data;
using PublishingHouse.Models;
using PublishingHouse.Repository.IRepository;

namespace PublishingHouse.Repository;

public class GenderRepository : Repository<Gender>, IGenderRepository
{
	public GenderRepository(AppDbContext db) : base(db)
	{ }
}
