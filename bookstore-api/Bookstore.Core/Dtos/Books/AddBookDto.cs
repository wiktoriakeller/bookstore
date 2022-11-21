using Bookstore.Core.Entities;

namespace Bookstore.Core.Dtos.Books
{
    public class AddBookDto
    {
        public string Title { get; init; }
        public string Author { get; init; }
        public string Description { get; init; }
        public string Genre { get; init; }
        public Reception Reception { get; init; }
        public Guid PublisherId { get; init; }
    }
}
