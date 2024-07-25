using PublishingHouse.Data;
using PublishingHouse.Models;
using PublishingHouse.Repository.IRepository;

namespace PublishingHouse.Repository
{
	public class Publisher : Repository<Publisher>, IPublisherRepository
	{
		public Publisher(AppDbContext db) : base(db)
		{ }
	}
}
