using PublishingHouse.Models.Dtos.ProductAuthor;

namespace PublishingHouse.Business.Services.Interfaces;

public interface IProductAuthorService
{
    Task<IEnumerable<ProductAuthorDto>> GetAll();
    Task<ProductAuthorDto> Get(int id);
    Task<ProductAuthorDto> Create(CreateProductAuthorDto entity);
    Task<ProductAuthorDto> Edit(int id, UpdateProductAuthorDto entity);
    Task<ProductAuthorDto> Remove(int id);
}
