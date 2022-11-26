namespace Bookstore.UI.Common.Models
{
    public record Publisher
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public override string ToString() => Name;
    }
}
