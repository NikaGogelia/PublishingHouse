using Microsoft.EntityFrameworkCore;
using PublishingHouse.Data;
using PublishingHouse.Models;
using PublishingHouse.Models.QueryParameterModel;
using PublishingHouse.Repository.IRepository;

namespace PublishingHouse.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
	private readonly AppDbContext _db;
	public ProductRepository(AppDbContext db) : base(db)
	{
		_db = db;
	}

	public async Task<IEnumerable<Product>> GetAllAsync(ProductQueryParameters queryParameters)
	{
		IQueryable<Product> query = _db.Products;

		// Filtering
		if (queryParameters.IsArchived == true)
		{
			query = query.Where(a => a.IsArchived == true);
		}
		else if (queryParameters.IsArchived == false)
		{
			query = query.Where(a => a.IsArchived == false);
		}
		else
		{
			query = query;
		}

		return await query.AsNoTracking().ToListAsync();
	}
}
