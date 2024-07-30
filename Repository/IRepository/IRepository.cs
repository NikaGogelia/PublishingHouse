using System.Linq.Expressions;

namespace PublishingHouse.Repository.IRepository;

public interface IRepository<T>
{
	Task<IEnumerable<T>> GetAllAsync();
	Task<T> GetByIdAsync(Expression<Func<T, bool>>? filter = null);
	Task AddAsync(T entity);
	Task DeleteAsync(int id);
	Task UpdateAsync(T entity);
}
