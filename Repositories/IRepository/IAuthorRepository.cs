using PublishingHouse.Models;
using PublishingHouse.Models.QueryParameterModel;

namespace PublishingHouse.Repository.IRepository;

public interface IAuthorRepository : IRepository<Author>
{
	Task<IEnumerable<Author>> GetAllAsync(AuthorQueryParameters queryParameters);
}
