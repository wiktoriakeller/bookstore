using Bookstore.Core.Dtos.Publishers;
using Refit;

namespace Bookstore.UI.ApiInterfaces
{
    public interface IPublishersApi
    {
        [Get("/api/v1/publishers/")]
        Task<IEnumerable<PublisherDto>> GetAllPublishers();

        [Get("/api/v1/publishers/filters?publishersNameFilter={publishersNameFilter}")]
        Task<IEnumerable<PublisherDto>> GetFilteredPublishers(string? publishersNameFilter);

        [Post("/api/v1/publishers/")]
        Task AddPublisher([Body] AddPublisherDto addPublisherDto);

        [Delete("/api/v1/publishers/{publisherId}")]
        Task DeletePublisher(Guid publisherId);

        [Put("/api/v1/publishers/{publisherId}")]
        Task UpdatePublisher(Guid publisherId, [Body] UpdatePublisherDto updatePublisherDto);
    }
}
