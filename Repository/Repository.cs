using Microsoft.EntityFrameworkCore;
using PublishingHouse.Data;
using PublishingHouse.Repository.IRepository;
using System.Linq.Expressions;

namespace PublishingHouse.Repository;

public class Repository<T> : IRepository<T> where T : class
{
	private readonly AppDbContext _db;
	private DbSet<T> dbSet;

	public Repository(AppDbContext db)
	{
		_db = db;
		dbSet = db.Set<T>();
	}

	public async Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null)
	{
		IQueryable<T> query = dbSet;

		if (includeProperties != null)
		{
			foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(property);
			}
		}

		return await query.ToListAsync();
	}

	public async Task<T> GetByIdAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
	{
		IQueryable<T> query = dbSet;

		if (filter != null)
		{
			query = query.Where(filter);
		}

		if (includeProperties != null)
		{
			foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(property);
			}
		}

		return await query.FirstOrDefaultAsync();
	}

	public async Task<T> AddAsync(T entity)
	{
		await dbSet.AddAsync(entity);
		await _db.SaveChangesAsync();

		return entity;
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var entiry = await dbSet.FindAsync(id);

		if (entiry != null)
		{
			dbSet.Remove(entiry);
			await _db.SaveChangesAsync();
			return true;
		}

		return false;
	}

	public async Task<T> UpdateAsync(int id, T entity)
	{
		dbSet.Update(entity);
		await _db.SaveChangesAsync();

		return entity;
	}
}
