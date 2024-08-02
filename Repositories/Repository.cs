using Microsoft.EntityFrameworkCore;
using PublishingHouse.Data;
using PublishingHouse.Repository.IRepository;
using System.Linq.Expressions;

namespace PublishingHouse.Repository;

public class Repository<T> : IRepository<T> where T : class
{
	private readonly AppDbContext _db;
	private readonly DbSet<T> _dbSet;

	public Repository(AppDbContext db)
	{
		_db = db;
		_dbSet = _db.Set<T>();
	}

	public async Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null)
	{
		IQueryable<T> query = _dbSet;

		if (includeProperties != null)
		{
			foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProp);
			}
		}

		return await query.AsNoTracking().ToListAsync();
	}

	public async Task<T> GetByIdAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
	{
		IQueryable<T> query = _dbSet;

		if (filter != null)
		{
			query = query.Where(filter);
		}

		if (!string.IsNullOrEmpty(includeProperties))
		{
			foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(property);
			}
		}

		return await query.AsNoTracking().FirstOrDefaultAsync();
	}

	public async Task<T> AddAsync(T entity)
	{
		await _dbSet.AddAsync(entity);

		return entity;
	}

	public async Task<T> DeleteAsync(int id)
	{
		var entity = await _dbSet.FindAsync(id);

		_dbSet.Remove(entity);

		return entity;
	}

	public async Task<T> UpdateAsync(T entity)
	{
		if (entity == null)
		{
			throw new ArgumentNullException(nameof(entity));
		}

		var entry = _db.Entry(entity);
		if (entry.State == EntityState.Detached)
		{
			_dbSet.Attach(entity);
			entry.State = EntityState.Modified;
		}
		else
		{
			_dbSet.Update(entity);
		}

		return entity;
	}
}