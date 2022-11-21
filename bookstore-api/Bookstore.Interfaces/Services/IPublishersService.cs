using Bookstore.Core.Dtos.Publishers;

namespace Bookstore.Interfaces.Services
{
    public interface IPublishersService
    {
        IEnumerable<PublisherDto> GetAllPublishers();

        Task<Guid> AddPublisher(AddPublisherDto addPublisherDto);

        Task UpdatePublisher(UpdatePublisherDto updatePublisherDto);

        Task DeletePublisher(DeletePublisherDto deletePublisherDto);
    }
}
