using Bookstore.Core.Common;

namespace Bookstore.Core.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public Reception Reception { get; set; }
        public Guid PublisherId { get; set; }
        public Publisher Publisher { get; set; }
    }
}
