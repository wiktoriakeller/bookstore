using Bookstore.Core.Dtos.Publishers;

namespace Bookstore.Interfaces.Services
{
    public interface IPublishersService
    {
        IEnumerable<PublisherDto> GetAllPublishers();

        IEnumerable<PublisherDto> FilterPublishers(PublishersFiltersDto publishersFiltersDto);

        Task<Guid> AddPublisher(AddPublisherDto addPublisherDto);

        Task UpdatePublisher(UpdatePublisherDto updatePublisherDto);

        Task DeletePublisher(DeletePublisherDto deletePublisherDto);
    }
}
