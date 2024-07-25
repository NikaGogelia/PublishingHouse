using PublishingHouse.Data;
using PublishingHouse.Repository.IRepository;

namespace PublishingHouse.Repository;

public class UnitOfWork : IUnitOfWork
{
	private readonly AppDbContext _db;

	public UnitOfWork(AppDbContext db)
	{
		_db = db;
	}
}
