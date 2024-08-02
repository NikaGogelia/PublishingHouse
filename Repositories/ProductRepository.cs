using PublishingHouse.Data;
using PublishingHouse.Models;
using PublishingHouse.Repository.IRepository;

namespace PublishingHouse.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
	public ProductRepository(AppDbContext db) : base(db)
	{ }
}
