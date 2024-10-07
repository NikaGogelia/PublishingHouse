using AutoMapper;
using PublishingHouse.Business.Services.Interfaces;
using PublishingHouse.Models;
using PublishingHouse.Models.Dtos.ProductAuthor;
using PublishingHouse.Repository.IRepository;

namespace PublishingHouse.Business.Services;

public class ProductAuthorService : IProductAuthorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProductAuthorService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductAuthorDto>> GetAll()
    {
        var productAuthors = await _unitOfWork.ProductAuthor.GetAllAsync(includeProperties: "Product,Author");

        var result = _mapper.Map<IEnumerable<ProductAuthorDto>>(productAuthors);

        return result;
    }

    public async Task<ProductAuthorDto> Get(int id)
    {
        var productAuthor = await _unitOfWork.ProductAuthor.GetByIdAsync(productAuthor => productAuthor.Id == id, includeProperties: "Product,Author");

        if (productAuthor == null)
        {
            throw new KeyNotFoundException($"ProductAuthor with id {id} not found.");
        }

        var result = _mapper.Map<ProductAuthorDto>(productAuthor);

        return result;
    }

    public async Task<ProductAuthorDto> Create(CreateProductAuthorDto entity)
    {
        var productAuthor = _mapper.Map<ProductAuthor>(entity);

        var create = await _unitOfWork.ProductAuthor.AddAsync(productAuthor);
        await _unitOfWork.Save();

        var result = _mapper.Map<ProductAuthorDto>(create);

        return result;
    }

    public async Task<ProductAuthorDto> Edit(int id, UpdateProductAuthorDto entity)
    {
        var check = await _unitOfWork.ProductAuthor.GetByIdAsync(productAuthor => productAuthor.Id == id);

        if (check == null)
        {
            throw new KeyNotFoundException($"ProductAuthor with id {id} not found.");
        }

        var productAuthor = _mapper.Map<ProductAuthor>(entity);

        productAuthor.Id = id;

        var update = await _unitOfWork.ProductAuthor.UpdateAsync(productAuthor);
        await _unitOfWork.Save();

        var result = _mapper.Map<ProductAuthorDto>(update);

        return result;
    }

    public async Task<ProductAuthorDto> Remove(int id)
    {
        var check = await _unitOfWork.ProductAuthor.GetByIdAsync(productAuthor => productAuthor.Id == id);

        if (check == null)
        {
            throw new KeyNotFoundException($"ProductAuthor with id {id} not found.");
        }

        var delete = await _unitOfWork.ProductAuthor.DeleteAsync(id);
        await _unitOfWork.Save();

        var result = _mapper.Map<ProductAuthorDto>(check);

        return result;
    }
}
