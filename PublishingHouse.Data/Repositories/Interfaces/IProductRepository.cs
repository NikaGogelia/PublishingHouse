using PublishingHouse.Models;
using PublishingHouse.Models.QueryParameterModel;


namespace PublishingHouse.Repository.IRepository;

public interface IProductRepository : IRepository<Product>
{
	Task<IEnumerable<Product>> GetAllAsync(ProductQueryParameters queryParameters);
}
