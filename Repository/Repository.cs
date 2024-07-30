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
		_db = db ?? throw new ArgumentNullException(nameof(db));
		_dbSet = _db.Set<T>();
	}

	public async Task<IEnumerable<T>> GetAllAsync()
	{
		try
		{
			return await _dbSet.AsNoTracking().ToListAsync();
		}
		catch (Exception ex)
		{
			throw new Exception("An error occurred while retrieving all records.", ex);
		}
	}

	public async Task<T> GetByIdAsync(Expression<Func<T, bool>>? filter = null)
	{
		try
		{
			IQueryable<T> query = _dbSet.AsNoTracking();

			if (filter != null)
			{
				query = query.Where(filter);
			}

			return await query.FirstOrDefaultAsync();
		}
		catch (Exception ex)
		{
			throw new Exception("An error occurred while retrieving the record.", ex);
		}
	}

	public async Task AddAsync(T entity)
	{
		try
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			await _dbSet.AddAsync(entity);
		}
		catch (Exception ex)
		{
			throw new Exception("An error occurred while adding the record.", ex);
		}
	}

	public async Task DeleteAsync(int id)
	{
		try
		{
			var entity = await _dbSet.FindAsync(id);
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			_dbSet.Remove(entity);
		}
		catch (Exception ex)
		{
			throw new Exception("An error occurred while deleting the record.", ex);
		}
	}

	public async Task UpdateAsync(T entity)
	{
		try
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			_dbSet.Update(entity);
		}
		catch (Exception ex)
		{
			throw new Exception("An error occurred while updating the record.", ex);
		}
	}
}