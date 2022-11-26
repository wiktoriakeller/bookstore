using Bookstore.Core.Common;

namespace Bookstore.Core.Dtos.Books
{
    public class BooksFiltersDto
    {
        public string? TitleFilter { get; init; }
        public DateTime PublishDateStart { get; init; }
        public DateTime PublishDateEnd { get; init; }
    }
}
