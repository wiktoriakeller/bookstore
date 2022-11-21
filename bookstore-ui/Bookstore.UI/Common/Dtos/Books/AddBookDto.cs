using Bookstore.UI.Common;

namespace Bookstore.Core.Dtos.Books
{
    public class AddBookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public Reception Reception { get; set; }
        public Guid PublisherId { get; set; }
    }
}
