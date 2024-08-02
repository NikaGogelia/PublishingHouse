using PublishingHouse.Models.Dtos.ProductType;

namespace PublishingHouse.Services.IServices;

public interface IProductTypeService
{
	Task<IEnumerable<ProductTypeDto>> GetAllProductType();
	Task<ProductTypeDto> GetProductType(int id);
	Task<ProductTypeDto> CreateProductType(CreateProductTypeDto entity);
}
