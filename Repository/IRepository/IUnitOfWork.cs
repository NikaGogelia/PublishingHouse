namespace PublishingHouse.Repository.IRepository;

public interface IUnitOfWork
{
	IAuthorRepository Author { get; }
	ICityRepository City { get; }
	ICounrtyRepository Counrty { get; }
	IGenderRepository Gender { get; }
	IProductRepository Product { get; }
	IProductTypeRepository ProductType { get; }
	IPublisherRepository Publisher { get; }
	public Task Save();
}
