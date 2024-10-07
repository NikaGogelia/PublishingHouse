using PublishingHouse.Data.Repositories.Interfaces;

namespace PublishingHouse.Repository.IRepository;

public interface IUnitOfWork
{
	IUserRepository User { get; }
	IAuthorRepository Author { get; }
	ICityRepository City { get; }
	ICountryRepository Country { get; }
	IGenderRepository Gender { get; }
	IProductRepository Product { get; }
	IProductTypeRepository ProductType { get; }
	IPublisherRepository Publisher { get; }
	IProductAuthorRepository ProductAuthor { get; }
	public Task Save();
}
