using Bookstore.UI.Common;

namespace Bookstore.Core.Dtos.Books
{
    public class BooksFiltersDto
    {
        public string? TitleFilter { get; set; }
        public Reception[] ReceptionFilters { get; set; } = Array.Empty<Reception>();
    }
}
