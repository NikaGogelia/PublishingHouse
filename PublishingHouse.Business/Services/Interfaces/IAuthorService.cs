using PublishingHouse.Models.Dtos.Author;
using PublishingHouse.Models.QueryParameterModel;

namespace PublishingHouse.Business.Services.Interfaces;

public interface IAuthorService
{
    Task<IEnumerable<AuthorDto>> GetAllAuthors(AuthorQueryParameters query);
    Task<AuthorByIdDto> GetAuthor(int id);
    Task<AuthorDto> CreateAuthor(CreateAuthorDto entity);
    Task<AuthorDto> EditAuthor(int id, UpdateAuthorDto entity);
    Task<AuthorDto> RemoveAuthor(int id);
}
