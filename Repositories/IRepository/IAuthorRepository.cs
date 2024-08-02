using PublishingHouse.Models;

namespace PublishingHouse.Repository.IRepository;

public interface IAuthorRepository : IRepository<Author>
{
	Task<IEnumerable<Author>> GetAllAsync(AuthorQueryParameters queryParameters);
}
