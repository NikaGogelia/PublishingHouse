using System.Linq.Expressions;

namespace PublishingHouse.Repository.IRepository;

public interface IRepository<T>
{
	Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null);
	Task<T> GetByIdAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
	Task<T> AddAsync(T entity);
	Task<T> DeleteAsync(int id);
	Task<T> UpdateAsync(T entity);
}
