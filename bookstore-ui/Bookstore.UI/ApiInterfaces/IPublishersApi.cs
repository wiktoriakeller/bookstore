using Bookstore.Core.Dtos.Publishers;
using Bookstore.UI.Common.Models;
using Refit;

namespace Bookstore.UI.ApiInterfaces
{
    public interface IPublishersApi
    {
        [Get("/api/v1/publishers/")]
        Task<IEnumerable<Publisher>> GetAllPublishers();

        [Get("/api/v1/publishers/filters?publishersNameFilter={publishersNameFilter}")]
        Task<IEnumerable<Publisher>> GetFilteredPublishers(string? publishersNameFilter);

        [Post("/api/v1/publishers/")]
        Task AddPublisher([Body] AddPublisherDto addPublisherDto);

        [Delete("/api/v1/publishers/{publisherId}")]
        Task DeletePublisher(Guid publisherId);

        [Put("/api/v1/publishers/{publisherId}")]
        Task UpdatePublisher(Guid publisherId, [Body] UpdatePublisherDto updatePublisherDto);
    }
}
