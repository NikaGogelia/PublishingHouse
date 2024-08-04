using Microsoft.EntityFrameworkCore;
using PublishingHouse.Data;
using PublishingHouse.Models;
using PublishingHouse.Models.QueryParameterModel;
using PublishingHouse.Repository.IRepository;

namespace PublishingHouse.Repository;

public class AuthorRepository : Repository<Author>, IAuthorRepository
{
	private readonly AppDbContext _db;
	public AuthorRepository(AppDbContext db) : base(db)
	{
		_db = db;
	}

	public async Task<IEnumerable<Author>> GetAllAsync(AuthorQueryParameters queryParameters)
	{
		IQueryable<Author> query = _db.Authors;

		// Filtering
		if (!string.IsNullOrEmpty(queryParameters.FirstName))
		{
			query = query.Where(a => a.FirstName.Contains(queryParameters.FirstName));
		}
		if (!string.IsNullOrEmpty(queryParameters.LastName))
		{
			query = query.Where(a => a.LastName.Contains(queryParameters.LastName));
		}
		if (!string.IsNullOrEmpty(queryParameters.PersonalNumber))
		{
			query = query.Where(a => a.PersonalNumber == queryParameters.PersonalNumber);
		}
		if (queryParameters.DateOfBirth.HasValue)
		{
			query = query.Where(a => a.DateOfBirth == queryParameters.DateOfBirth.Value);
		}
		if (!string.IsNullOrEmpty(queryParameters.PhoneNumber))
		{
			query = query.Where(a => a.PhoneNumber.Contains(queryParameters.PhoneNumber));
		}
		if (!string.IsNullOrEmpty(queryParameters.Email))
		{
			query = query.Where(a => a.Email.Contains(queryParameters.Email));
		}

		// Pagination
		query = query.Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
					 .Take(queryParameters.PageSize);

		return await query.AsNoTracking().ToListAsync();
	}
}
