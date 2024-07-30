using PublishingHouse.Data;
using PublishingHouse.Models;
using PublishingHouse.Repository.IRepository;

namespace PublishingHouse.Repository;

public class ProductAuthorRepository : Repository<ProductAuthor>, IProductAuthorRepository
{
	public ProductAuthorRepository(AppDbContext db) : base(db)
	{ }
}
