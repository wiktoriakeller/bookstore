using Bookstore.Core.Common;

namespace Bookstore.Core.Dtos.Books
{
    public class UpdateBookDto
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Author { get; init; }
        public string Description { get; init; }
        public string Genre { get; init; }
        public Reception Reception { get; init; }
        public Guid PublisherId { get; init; }
    }
}
