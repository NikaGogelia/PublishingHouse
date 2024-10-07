using AutoMapper;
using PublishingHouse.Business.Services.Interfaces;
using PublishingHouse.Models;
using PublishingHouse.Models.Dtos.ProductType;
using PublishingHouse.Repository.IRepository;

namespace PublishingHouse.Business.Services;

public class ProductTypeService : IProductTypeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProductTypeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductTypeDto>> GetAllProductType()
    {
        var productTypes = await _unitOfWork.ProductType.GetAllAsync();

        var result = _mapper.Map<IEnumerable<ProductTypeDto>>(productTypes);

        return result;
    }

    public async Task<ProductTypeDto> GetProductType(int id)
    {
        var productType = await _unitOfWork.ProductType.GetByIdAsync(productType => productType.Id == id);

        if (productType == null)
        {
            throw new KeyNotFoundException($"Product type with id {id} not found.");
        }

        var result = _mapper.Map<ProductTypeDto>(productType);

        return result;
    }

    public async Task<ProductTypeDto> CreateProductType(CreateProductTypeDto entity)
    {
        var productType = _mapper.Map<ProductType>(entity);

        var create = await _unitOfWork.ProductType.AddAsync(productType);
        await _unitOfWork.Save();

        var result = _mapper.Map<ProductTypeDto>(create);

        return result;
    }
}
