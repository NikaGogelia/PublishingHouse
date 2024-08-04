using PublishingHouse.Models;

namespace PublishingHouse.Repository.IRepository;

public interface IProductRepository : IRepository<Product>
{
	Task<IEnumerable<Product>> GetAllAsync(ProductQueryParameters queryParameters);
}
