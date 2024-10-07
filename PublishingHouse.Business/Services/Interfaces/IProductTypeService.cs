using PublishingHouse.Models.Dtos.ProductType;

namespace PublishingHouse.Business.Services.Interfaces;

public interface IProductTypeService
{
    Task<IEnumerable<ProductTypeDto>> GetAllProductType();
    Task<ProductTypeDto> GetProductType(int id);
    Task<ProductTypeDto> CreateProductType(CreateProductTypeDto entity);
}
