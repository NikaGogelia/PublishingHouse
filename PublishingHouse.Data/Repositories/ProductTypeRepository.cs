using PublishingHouse.Data;
using PublishingHouse.Models;
using PublishingHouse.Repository.IRepository;

namespace PublishingHouse.Repository;

public class ProductTypeRepository : Repository<ProductType>, IProductTypeRepository
{
	public ProductTypeRepository(AppDbContext db) : base(db)
	{ }
}
