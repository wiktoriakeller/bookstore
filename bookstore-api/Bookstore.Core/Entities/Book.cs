namespace Bookstore.Core.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public Reception Reception { get; set; }
        public Publisher Publisher { get; set; }
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    }
}
