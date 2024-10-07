using PublishingHouse.Data;
using PublishingHouse.Models;
using PublishingHouse.Repository.IRepository;

namespace PublishingHouse.Repository;

public class PublisherRepository : Repository<Publisher>, IPublisherRepository
{
	public PublisherRepository(AppDbContext db) : base(db)
	{ }
}
