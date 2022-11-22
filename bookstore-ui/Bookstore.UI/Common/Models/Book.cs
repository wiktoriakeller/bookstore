using Bookstore.Core.Models;

namespace Bookstore.UI.Common.Models
{
    public class Book
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Author { get; init; }
        public string Description { get; init; }
        public string Genre { get; init; }
        public Reception Reception { get; init; }
        public Guid PublisherId { get; init; }
        public Publisher Publisher { get; init; }
    }
}
