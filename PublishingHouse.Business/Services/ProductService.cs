using AutoMapper;
using PublishingHouse.Business.Services.Interfaces;
using PublishingHouse.Models;
using PublishingHouse.Models.Dtos.Product;
using PublishingHouse.Models.QueryParameterModel;
using PublishingHouse.Repository.IRepository;

namespace PublishingHouse.Business.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProducts(ProductQueryParameters query)
    {
        var products = await _unitOfWork.Product.GetAllAsync(queryParameters: query);

        var result = _mapper.Map<IEnumerable<ProductDto>>(products);

        return result;
    }

    public async Task<ProductByIdDto> GetProduct(int id)
    {
        var product = await _unitOfWork.Product.GetByIdAsync(product => product.Id == id, includeProperties: "Publisher,ProductType,ProductAuthors.Author");

        if (product == null)
        {
            throw new KeyNotFoundException($"Product with id {id} not found.");
        }

        var result = _mapper.Map<ProductByIdDto>(product);

        return result;
    }

    public async Task<ProductDto> ArchiveProduct(int id, UpdateProductDto entity)
    {
        var check = await _unitOfWork.Product.GetByIdAsync(product => product.Id == id);

        if (check == null)
        {
            throw new KeyNotFoundException($"Product with id {id} not found.");
        }

        var product = _mapper.Map<Product>(entity);

        product.Id = id;
        product.Name = check.Name;
        product.Annotation = check.Annotation;
        product.ProductTypeId = check.ProductTypeId;
        product.ISBN = check.ISBN;
        product.ReleaseDate = check.ReleaseDate;
        product.PublisherId = check.PublisherId;
        product.NumberOfPages = check.NumberOfPages;
        product.Address = check.Address;
        product.IsArchived = entity.IsArchived;

        var update = await _unitOfWork.Product.UpdateAsync(product);
        await _unitOfWork.Save();

        var result = _mapper.Map<ProductDto>(update);

        return result;
    }

    public async Task<ProductDto> CreateProduct(CreateProductDto entity)
    {
        var product = _mapper.Map<Product>(entity);

        var create = await _unitOfWork.Product.AddAsync(product);
        await _unitOfWork.Save();

        var result = _mapper.Map<ProductDto>(create);

        return result;
    }
}
