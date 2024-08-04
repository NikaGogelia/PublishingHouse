using PublishingHouse.Models;
using PublishingHouse.Models.Dtos.Product;

namespace PublishingHouse.Services.IServices;

public interface IProductService
{
	Task<IEnumerable<ProductDto>> GetAllProducts(ProductQueryParameters query);
	Task<ProductByIdDto> GetProduct(int id);
	Task<ProductDto> CreateProduct(CreateProductDto entity);
	Task<ProductDto> ArchiveProduct(int id, UpdateProductDto entity);
}
