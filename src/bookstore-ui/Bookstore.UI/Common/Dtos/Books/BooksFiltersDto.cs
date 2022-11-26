using Bookstore.UI.Common;

namespace Bookstore.Core.Dtos.Books
{
    public class BooksFiltersDto
    {
        public string? TitleFilter { get; set; }
        public DateTime PublishDateStart { get; set; }
        public DateTime PublishDateEnd { get; set; }
    }
}
