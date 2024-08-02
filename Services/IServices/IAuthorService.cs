using PublishingHouse.Models.Dtos.Author;

namespace PublishingHouse.Services.IServices;

public interface IAuthorService
{
	Task<IEnumerable<AuthorDto>> GetAllAuthors();
	Task<AuthorDto> GetAuthor(int id);
	Task<AuthorDto> CreateAuthor(CreateAuthorDto entity);
	Task<AuthorDto> RemoveAuthor(int id);
	Task<AuthorDto> EditAuthor(int id, UpdateAuthorDto entity);
}
