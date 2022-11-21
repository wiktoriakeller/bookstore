using Bookstore.Core.Dtos.Publishers;
using Bookstore.UI.Common;

namespace Bookstore.Core.Dtos.Books
{
    public class BookDto
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Author { get; init; }
        public string Description { get; init; }
        public string Genre { get; init; }
        public Reception Reception { get; init; }
        public Guid PublisherId { get; init; }
        public PublisherDto Publisher { get; init; }
    }
}
