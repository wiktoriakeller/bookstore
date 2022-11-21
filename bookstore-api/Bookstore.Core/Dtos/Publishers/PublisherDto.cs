using Bookstore.Core.Dtos.Books;

namespace Bookstore.Core.Dtos.Publishers
{
    public class PublisherDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public ICollection<BookDto> Books { get; init; }
    }
}
