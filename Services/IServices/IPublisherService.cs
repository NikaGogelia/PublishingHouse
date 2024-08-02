using PublishingHouse.Models.Dtos.Publisher;

namespace PublishingHouse.Services.IServices;

public interface IPublisherService
{
	Task<IEnumerable<PublisherDto>> GetAllPublishers();
	Task<PublisherDto> GetPublisher(int id);
	Task<PublisherDto> CreatePublisher(CreatePublisherDto entity);
	Task<PublisherDto> EditPublisher(int id, UpdatePublisherDto entity);
	Task<PublisherDto> RemovePublisher(int id);
}
