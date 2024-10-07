using AutoMapper;
using PublishingHouse.Business.Services.Interfaces;
using PublishingHouse.Models;
using PublishingHouse.Models.Dtos.Publisher;
using PublishingHouse.Repository.IRepository;

namespace PublishingHouse.Business.Services;

public class PublisherService : IPublisherService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public PublisherService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PublisherDto>> GetAllPublishers()
    {
        var publishers = await _unitOfWork.Publisher.GetAllAsync();

        var result = _mapper.Map<IEnumerable<PublisherDto>>(publishers);

        return result;
    }

    public async Task<PublisherDto> GetPublisher(int id)
    {
        var publisher = await _unitOfWork.Publisher.GetByIdAsync(publisher => publisher.Id == id);

        if (publisher == null)
        {
            throw new KeyNotFoundException($"publisher with id {id} not found.");
        }

        var result = _mapper.Map<PublisherDto>(publisher);

        return result;
    }

    public async Task<PublisherDto> CreatePublisher(CreatePublisherDto entity)
    {
        var publisher = _mapper.Map<Publisher>(entity);

        var create = await _unitOfWork.Publisher.AddAsync(publisher);
        await _unitOfWork.Save();

        var result = _mapper.Map<PublisherDto>(create);

        return result;
    }

    public async Task<PublisherDto> EditPublisher(int id, UpdatePublisherDto entity)
    {
        var check = await _unitOfWork.Publisher.GetByIdAsync(publisher => publisher.Id == id);

        if (check == null)
        {
            throw new KeyNotFoundException($"publisher with id {id} not found.");
        }

        var publisher = _mapper.Map<Publisher>(entity);

        publisher.Id = id;

        var update = await _unitOfWork.Publisher.UpdateAsync(publisher);
        await _unitOfWork.Save();

        var result = _mapper.Map<PublisherDto>(update);

        return result;
    }

    public async Task<PublisherDto> RemovePublisher(int id)
    {
        var check = await _unitOfWork.Publisher.GetByIdAsync(publisher => publisher.Id == id);

        if (check == null)
        {
            throw new KeyNotFoundException($"Publisher with id {id} not found.");
        }

        var delete = await _unitOfWork.Publisher.DeleteAsync(id);
        await _unitOfWork.Save();

        var result = _mapper.Map<PublisherDto>(check);

        return result;
    }
}
