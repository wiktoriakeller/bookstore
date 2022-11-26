namespace Bookstore.UI.Common.Models
{
    public record Book
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Author { get; init; }
        public string Genre { get; init; }
        public Reception Reception { get; init; }
        public DateTime PublishDate { get; init; }
        public Guid PublisherId { get; init; }
        public Publisher Publisher { get; init; }
    }
}
