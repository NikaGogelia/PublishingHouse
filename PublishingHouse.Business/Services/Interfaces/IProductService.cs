using PublishingHouse.Models.Dtos.Product;
using PublishingHouse.Models.QueryParameterModel;

namespace PublishingHouse.Business.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProducts(ProductQueryParameters query);
    Task<ProductByIdDto> GetProduct(int id);
    Task<ProductDto> CreateProduct(CreateProductDto entity);
    Task<ProductDto> ArchiveProduct(int id, UpdateProductDto entity);
}
