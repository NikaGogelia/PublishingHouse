using AutoMapper;
using PublishingHouse.Business.Services.Interfaces;
using PublishingHouse.Models;
using PublishingHouse.Models.Dtos.Author;
using PublishingHouse.Models.QueryParameterModel;
using PublishingHouse.Repository.IRepository;

namespace PublishingHouse.Business.Services;

public class AuthorService : IAuthorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AuthorDto>> GetAllAuthors(AuthorQueryParameters query)
    {
        var authors = await _unitOfWork.Author.GetAllAsync(queryParameters: query);

        var result = _mapper.Map<IEnumerable<AuthorDto>>(authors);

        return result;
    }

    public async Task<AuthorByIdDto> GetAuthor(int id)
    {
        var author = await _unitOfWork.Author.GetByIdAsync(author => author.Id == id, includeProperties: "Gender,Country,City,ProductAuthors.Product");

        if (author == null)
        {
            throw new KeyNotFoundException($"Author with id {id} not found.");
        }

        var result = _mapper.Map<AuthorByIdDto>(author);

        return result;
    }

    public async Task<AuthorDto> CreateAuthor(CreateAuthorDto entity)
    {
        var author = _mapper.Map<Author>(entity);

        var create = await _unitOfWork.Author.AddAsync(author);
        await _unitOfWork.Save();

        var result = _mapper.Map<AuthorDto>(create);

        return result;
    }

    public async Task<AuthorDto> EditAuthor(int id, UpdateAuthorDto entity)
    {
        var check = await _unitOfWork.Author.GetByIdAsync(author => author.Id == id);

        if (check == null)
        {
            throw new KeyNotFoundException($"Author with id {id} not found.");
        }

        var author = _mapper.Map<Author>(entity);

        author.Id = id;

        var update = await _unitOfWork.Author.UpdateAsync(author);
        await _unitOfWork.Save();

        var result = _mapper.Map<AuthorDto>(update);

        return result;
    }

    public async Task<AuthorDto> RemoveAuthor(int id)
    {
        var check = await _unitOfWork.Author.GetByIdAsync(author => author.Id == id);

        if (check == null)
        {
            throw new KeyNotFoundException($"Author with id {id} not found.");
        }

        var delete = await _unitOfWork.Author.DeleteAsync(id);
        await _unitOfWork.Save();

        var result = _mapper.Map<AuthorDto>(check);

        return result;
    }
}
