using PublishingHouse.Data;
using PublishingHouse.Data.Repositories;
using PublishingHouse.Data.Repositories.Interfaces;
using PublishingHouse.Repository.IRepository;

namespace PublishingHouse.Repository;

public class UnitOfWork : IUnitOfWork
{
	private readonly AppDbContext _db;
	public IUserRepository User { get; private set; }
	public IAuthorRepository Author { get; private set; }
	public ICityRepository City { get; private set; }
	public ICountryRepository Country { get; private set; }
	public IGenderRepository Gender { get; private set; }
	public IProductRepository Product { get; private set; }
	public IProductTypeRepository ProductType { get; private set; }
	public IPublisherRepository Publisher { get; private set; }
	public IProductAuthorRepository ProductAuthor { get; private set; }

	public UnitOfWork(AppDbContext db)
	{
		_db = db;
		User = new UserRepository(_db);
		Author = new AuthorRepository(_db);
		City = new CityRepository(_db);
		Country = new CountryRepository(_db);
		Gender = new GenderRepository(_db);
		Product = new ProductRepository(_db);
		ProductType = new ProductTypeRepository(_db);
		Publisher = new PublisherRepository(_db);
		ProductAuthor = new ProductAuthorRepository(_db);
	}

	public Task Save()
	{
		return _db.SaveChangesAsync();
	}
}
